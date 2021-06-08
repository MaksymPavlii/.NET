using System;

namespace MatrixLibrary
{
    public class MatrixException : Exception
    {
        public MatrixException(string message)
            : base(message)
        { }

    }

    public class Matrix : ICloneable
    {
        readonly double[,] matrix;


        public Matrix(int rows, int columns)
        {
            if (rows < 0 || columns < 0)
                throw new ArgumentOutOfRangeException("rows");
            this.matrix = new double[rows, columns];
        }
        public Matrix(double[,] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException("matrix");
            this.matrix = matrix;
        }

        public int Rows { get { return matrix.GetLength(0); } }
        public int Columns { get {return matrix.GetLength(1); } }

        public double[,] Array
        {
            get
            {
                return matrix;
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i < 0 || i > Rows || j < 0 || j > Columns)
                    throw new ArgumentException("i");
                return matrix[i, j];
            }
            set
            {
                if (i < 0 || i > Rows || j < 0 || j > Columns)
                    throw new ArgumentException("i");
                matrix[i, j] = value;
            }
            
        }

        public object Clone()
        {

            var copy = matrix.Clone() as double[,];
            return new Matrix(copy);

        }

        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null)
            {
                throw new ArgumentNullException("matrix1","ArgumentNullException");
            }
            return matrix1.Add(matrix2);
        }

        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null || matrix2 == null)
            {
                throw new ArgumentNullException("matrix1","null");
            }
            if (matrix1.matrix.GetLength(0) != matrix2.matrix.GetLength(0) || matrix1.matrix.GetLength(1) != matrix2.matrix.GetLength(1))
            {
                throw new MatrixException("MatricesHaveInappropriateDimensions");
            }
            return matrix1.Subtract(matrix2);
        }

        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1 == null)
            {
                throw new ArgumentNullException("matrix1","ArgumentNullException");
            }
            return matrix1.Multiply(matrix2);
        }

        public Matrix Add(Matrix matrix)
        {
            if (this.matrix == null || matrix == null)
            {
                throw new ArgumentNullException("matrix","ArgumentNullException");
            }
            if (this.matrix.GetLength(0) != matrix.matrix.GetLength(0) || this.matrix.GetLength(1) != matrix.matrix.GetLength(1))
            {
                throw new MatrixException("MatricesHaveInappropriateDimensions");
            }
            Matrix result = new Matrix(Rows, Columns);
            if (this.Rows == matrix.Rows && this.Columns == matrix.Columns)
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        result.matrix[i, j] = this.matrix[i, j] + matrix[i, j];
                    }
                }
            }

            return result;
        }

        public Matrix Subtract(Matrix matrix)
        {
            if (this.matrix == null || matrix == null)
            {
                throw new ArgumentNullException("matrix","ArgumentNullException");
            }
            if (this.matrix.GetLength(0) != matrix.matrix.GetLength(0) || this.matrix.GetLength(1) != matrix.matrix.GetLength(1))
            {
                throw new MatrixException("MatricesHaveInappropriateDimensions");
            }

            Matrix result = new Matrix(Rows, Columns);

            if (this.Rows == matrix.Rows && this.Columns == matrix.Columns)
            {

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        result.matrix[i, j] = this.matrix[i, j] - matrix[i, j];
                    }
                }
            }

            return result;
        }

        public Matrix Multiply(Matrix matrix)
        {
            if (this.matrix == null || matrix == null)
            {
                throw new ArgumentNullException("matrix","ArgumentNullException");
            }
            if((this.Columns != matrix.Rows))
                throw new MatrixException("MatricesHaveInappropriateDimensions");

            if (this.Columns == matrix.Rows)
            {
                Matrix result = new Matrix(Rows, matrix.Columns);

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < matrix.Columns; j++)
                    {
                        for (int k = 0; k < Columns; k++)
                        {

                            result.matrix[i, j] += this[i, k] * matrix[k, j];
                        }
                    }
                }
                return result;
            }
            return null;

        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            bool isEqual = false;
            if (obj.GetType() != this.GetType()) return false;
            Matrix matrix1 = (Matrix)obj;

            if (matrix1.matrix.GetLength(0) == matrix.GetLength(0) && matrix1.matrix.GetLength(1) == matrix.GetLength(1))
            {
                for (int counter1 = 0; counter1 < matrix1.matrix.GetLength(0); counter1++)
                {
                    for (int counter2 = 0; counter2 < matrix.GetLength(1); counter2++)
                    {
                        if (matrix1[counter1, counter2] == this.matrix[counter1, counter2] )
                        {
                            isEqual = true;
                        }
                        else return false;
                    }
                }
            }         
            return isEqual;
        }

      
        public override int GetHashCode() => base.GetHashCode();
    }              
 }

