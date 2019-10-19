using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;

namespace UnscentedKalmanFilter
{
    /// <summary>
    /// Kalman Algorithm Implementation
    /// </summary>
    public class Ukf
    {
        /// <summary>
        /// States number
        /// </summary>
        private int L;

        /// <summary>
        /// Measurements number
        /// </summary>
        private int m;

        /// <summary>
        /// The alpha coefficient, characterize sigma-points dispersion around mean
        /// </summary>
        private double alpha;

        /// <summary>
        /// The ki.
        /// </summary>
        private double ki;

        /// <summary>
        /// The beta coefficient, characterize type of distribution (2 for normal one) 
        /// </summary>
        private double beta;

        /// <summary>
        /// Scale factor
        /// </summary>
        private double lambda;

        /// <summary>
        /// Scale factor
        /// </summary>
        private double c;

        /// <summary>
        /// Means weights
        /// </summary>
        private Matrix<double> Wm;

        /// <summary>
        /// Covariance weights
        /// </summary>
        private Matrix<double> Wc;

        /// <summary>
		/// State
		/// </summary>
        private Matrix<double> x;

        /// <summary>
		/// Covariance
		/// </summary>
        private Matrix<double> P;

        /// <summary>
		/// Std of process 
		/// </summary>
        private double q;

        /// <summary>
		/// Std of measurement 
		/// </summary>
        private double r;

        /// <summary>
		/// Covariance of process
		/// </summary>
        private Matrix<double> Q;

        /// <summary>
		/// Covariance of measurement 
		/// </summary>
        private Matrix<double> R;

        /// <summary>
        /// Alt Measurement Values
        /// </summary>
        private double[] MeasurementsAlt;

        private Matrix<double> StateFactor;

        /// <summary>
        /// Constructor of Unscented Kalman Filter
        /// </summary>
        /// <param name="L">States number</param>
        /// <param name="m">Measurements number</param>
        public Ukf(int L = 0)
        {
            this.L = L;
        }

        /// <summary>
        /// Initial Kalman Values
        /// </summary>
        private void Init()
        {
            q = 20;
            r = 0.3;

            x = q * Matrix.Build.Random(L, 1); //initial state with noise
            P = Matrix.Build.Diagonal(L, L, 1); //initial state covraiance

            Q = Matrix.Build.Diagonal(L, L, q * q); //covariance of process
            R = Matrix.Build.Dense(m, m, r * r); //covariance of measurement  

            alpha = 1e-3f;
            ki = 0;
            beta = 2f;
            lambda = alpha * alpha * (L + ki) - L;
            c = L + lambda;

            //weights for means
            Wm = Matrix.Build.Dense(1, (2 * L + 1), 0.5 / c);
            Wm[0, 0] = lambda / c;

            //weights for covariance
            Wc = Matrix.Build.Dense(1, (2 * L + 1));
            Wm.CopyTo(Wc);
            Wc[0, 0] = Wm[0, 0] + 1 - alpha * alpha + beta;

            c = Math.Sqrt(c);
        }

        /// <summary>
        /// Kalman Update Method
        /// </summary>
        /// <param name="measurements"></param>
        public void Update(double[] measurements)
        {
            MeasurementsAlt = measurements;

            if (m == 0)
            {
                var mNum = measurements.Length;
                if (mNum > 0)
                {
                    m = mNum;
                    if (L == 0) L = mNum;
                    Init();
                }
            }

            var z = Matrix.Build.Dense(m, 1, 0);
            z.SetColumn(0, measurements);

            //sigma points around x
            var X = GetSigmaPoints(x, P, c);

            //unscented transformation of process
            // X1=sigmas(x1,P1,c) - sigma points around x1
            //X2=X1-x1(:,ones(1,size(X1,2))) - deviation of X1
            var ut_f_matrices = UnscentedTransform(X, Wm, Wc, L, Q);
            var x1 = ut_f_matrices[0];
            var X1 = ut_f_matrices[1];
            var P1 = ut_f_matrices[2];
            var X2 = ut_f_matrices[3];

            //unscented transformation of measurments
            var ut_h_matrices = UnscentedTransform(X1, Wm, Wc, m, R);
            var z1 = ut_h_matrices[0];
            var Z1 = ut_h_matrices[1];
            var P2 = ut_h_matrices[2];
            var Z2 = ut_h_matrices[3];

            //transformed cross-covariance
            var P12 = (X2.Multiply(Matrix.Build.Diagonal(Wc.Row(0).ToArray()))).Multiply(Z2.Transpose());

            var K = P12.Multiply(P2.Inverse());

            //state update
            StateFactor = K.Multiply(z.Subtract(z1));

            x = x1.Add(StateFactor);
            //covariance update 
            P = P1.Subtract(K.Multiply(P12.Transpose()));
        }

        /// <summary>
        /// Kalman Predict Method
        /// </summary>
        public void Predict()
        {
            if (MeasurementsAlt == null) return;

            x = x.Add(StateFactor);
        }


        public double[] CorrectOldData(double[] measurements, int times, double[] oldPredictedState, double[,] oldCovariance)
        {
            if (measurements == null) return null;

            // restore Kalman to the old state
            if (x == null)
            {
                x = Matrix.Build.Dense(oldPredictedState.Length, 1);
            }
            if (P == null)
            {
                P = Matrix.Build.Dense(oldPredictedState.Length, oldPredictedState.Length);
            }


            for (var i = 0; i < oldPredictedState.Length; i++)
            {
                x[i, 0] = oldPredictedState[i];
            }

            for (var i = 0; i < Math.Sqrt(oldCovariance.Length); i++)
            {
                for (var j = 0; j < Math.Sqrt(oldCovariance.Length); j++)
                {
                    P[i, j] = oldCovariance[i, j];
                }
            }


            var result = new double[measurements.Length * times];

            // update Kallmann filter n times and store the results in the result array

            // the first update depends on the measurements
            // and the rest depend on prediction 
            Update(measurements);

            for (var j = 0; j < measurements.Length; j++)
            {
                result[j] = GetState()[j];
            }

            for (var i = 1; i < times; i++)
            {
                Predict();

                for (var j = 0; j < measurements.Length; j++)
                {
                    result[i * measurements.Length + j] = GetState()[j];
                }
            }

            return result;
        }

        public double[] GetState()
        {
            if (x == null) return null;
            return x.ToColumnArrays()[0];
        }

        public double[,] GetCovariance()
        {
            return P.ToArray();
        }

        /// <summary>
        /// Unscented Transformation
        /// </summary>
        /// <param name="f">nonlinear map</param>
        /// <param name="X">sigma points</param>
        /// <param name="Wm">Weights for means</param>
        /// <param name="Wc">Weights for covariance</param>
        /// <param name="n">numer of outputs of f</param>
        /// <param name="R">additive covariance</param>
        /// <returns>[transformed mean, transformed smapling points, transformed covariance, transformed deviations</returns>
        private Matrix<double>[] UnscentedTransform(Matrix<double> X, Matrix<double> Wm, Matrix<double> Wc, int n, Matrix<double> R)
        {
            var L = X.ColumnCount;
            var y = Matrix.Build.Dense(n, 1, 0);
            var Y = Matrix.Build.Dense(n, L, 0);

            for (var k = 0; k < L; k++)
            {
                var row_in_X = X.SubMatrix(0, X.RowCount, k, 1);
                Y.SetSubMatrix(0, Y.RowCount, k, 1, row_in_X);
                y = y.Add(Y.SubMatrix(0, Y.RowCount, k, 1).Multiply(Wm[0, k]));
            }

            var Y1 = Y.Subtract(y.Multiply(Matrix.Build.Dense(1, L, 1)));
            var P = Y1.Multiply(Matrix.Build.Diagonal(Wc.Row(0).ToArray()));
            P = P.Multiply(Y1.Transpose());
            P = P.Add(R);

            Matrix<double>[] output = { y, Y, P, Y1 };
            return output;
        }

        /// <summary>
        /// Sigma points around reference point
        /// </summary>
        /// <param name="x">reference point</param>
        /// <param name="P">covariance</param>
        /// <param name="c">coefficient</param>
        /// <returns>Sigma points</returns>
        private Matrix<double> GetSigmaPoints(Matrix<double> x, Matrix<double> P, double c)
        {
            var A = P.Cholesky().Factor;
            //P= A A^T
            // [ 1 2 3]    [x 0 0]
            // [ 4 5 6] -> [x x 0]
            // [ 7 8 9]    [x x x]
            A = A.Multiply(c);
            A = A.Transpose();

            var n = x.RowCount;

            var Y = Matrix.Build.Dense(n, n, 1);
            for (var j = 0; j < n; j++)
            {
                Y.SetSubMatrix(0, n, j, 1, x);
            }

            var X = Matrix.Build.Dense(n, (2 * n + 1));
            X.SetSubMatrix(0, n, 0, 1, x);

            var Y_plus_A = Y.Add(A);
            X.SetSubMatrix(0, n, 1, n, Y_plus_A);

            var Y_minus_A = Y.Subtract(A);
            X.SetSubMatrix(0, n, n + 1, n, Y_minus_A);

            return X;
        }
    }
}

