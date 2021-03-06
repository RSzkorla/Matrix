﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Matrix
{
  public class Matrix
  {
    private double[,] matrix;
    public int rowCount { get; private set; }
    public int colCount { get; private set; }

    public double this[int row, int col]
    {
      get
      {
        return matrix[row, col];
      }
      set
      {
        SetValue(row, col, value);
      }
    }
    public double det {
      get
      {
        return det;
      }
      private set
      {
        SetDet();
      } }



    private Random rng = new Random();
    #region Constructors

    public Matrix()
    {
      rowCount = 0;
      colCount = 0;
      matrix = new double[0, 0];

    }
    public Matrix(int row, int col)
    {
      rowCount = row;
      colCount = col;
      matrix = new double[row, col];
    }

    public Matrix(int n)
    {
      rowCount = n;
      colCount = n;
      matrix = new double[n, n];
    }
    #endregion

    #region Fillers
    public void Reset()
    {
      for (int i = 0; i < rowCount; i++)
      {
        for (int j = 0; j < colCount; j++)
        {
          matrix[i, j] = 0;
        }
      }
    }

    public void Fill(double l)
    {
      for (int i = 0; i < rowCount; i++)
      {
        for (int j = 0; j < colCount; j++)
        {
          matrix[i, j] = l;
        }
      }
    }

    public void IdentyFill()
    {
      if (rowCount != colCount)
      {
        throw new ArrayTypeMismatchException("Matrix is not square");
      }
      this.Reset();
      for (int i = 0; i < rowCount; i++)
      {

        matrix[i, i] = 1;
      }
    }

    public void RandomFill()
    {
      for (int i = 0; i < rowCount; i++)
      {
        for (int j = 0; j < colCount; j++)
        {
          matrix[i, j] = rng.NextDouble();
        }
      }
    }

    public void RandomIntFill(int min, int max)
    {
      for (int i = 0; i < rowCount; i++)
      {
        for (int j = 0; j < colCount; j++)
        {
          matrix[i, j] = rng.Next(min, max);
        }
      }
    }
    #endregion

    #region Operators
    public static Matrix operator +(Matrix lmat, Matrix rmat)
    {
      if (lmat.rowCount != rmat.rowCount || lmat.colCount != rmat.colCount)
      {
        throw new ArrayTypeMismatchException("Dimensions are not the same");
      }
      Matrix temp = new Matrix(rmat.rowCount, rmat.colCount);
      for (int i = 0; i < temp.rowCount; i++)
      {
        for (int j = 0; j < temp.colCount; j++)
        {
          temp.matrix[i, j] = lmat.matrix[i, j] + rmat.matrix[i, j];
        }
      }
      return temp;
    }
    public static Matrix operator -(Matrix lmat, Matrix rmat)
    {
      if (lmat.rowCount != rmat.rowCount || lmat.colCount != rmat.colCount)
      {
        throw new ArrayTypeMismatchException("Dimensions are not the same");
      }
      Matrix temp = new Matrix(rmat.rowCount, rmat.colCount);
      for (int i = 0; i < temp.rowCount; i++)
      {
        for (int j = 0; j < temp.colCount; j++)
        {
          temp.matrix[i, j] = lmat.matrix[i, j] - rmat.matrix[i, j];
        }
      }
      return temp;
    }

    public static Matrix operator *(Matrix mat, double l)
    {
      for (int i = 0; i < mat.rowCount; i++)
      {
        for (int j = 0; j < mat.colCount; j++)
        {
          mat.matrix[i, j] = mat.matrix[i, j] * l;
        }
      }
      return mat;
    }
    public static Matrix operator *(double l, Matrix mat)
    {
      for (int i = 0; i < mat.rowCount; i++)
      {
        for (int j = 0; j < mat.colCount; j++)
        {
          mat.matrix[i, j] = mat.matrix[i, j] * l;
        }
      }
      return mat;
    }
    public static Matrix operator /(Matrix mat, double l)
    {
      for (int i = 0; i < mat.rowCount; i++)
      {
        for (int j = 0; j < mat.colCount; j++)
        {
          mat.matrix[i, j] = mat.matrix[i, j] / l;
        }
      }
      return mat;
    }
    public static Matrix operator /(double l, Matrix mat)
    {
      for (int i = 0; i < mat.rowCount; i++)
      {
        for (int j = 0; j < mat.colCount; j++)
        {
          mat.matrix[i, j] = mat.matrix[i, j] / l;
        }
      }
      return mat;
    }
    public static Matrix operator * ( Matrix lmat, Matrix rmat)
    {

      if (lmat.colCount != rmat.rowCount)
      {
        throw new ArrayTypeMismatchException("Dimensions are not correct");
      }

      Matrix temp = new Matrix(lmat.rowCount, rmat.colCount);

      for (int i = 0; i < temp.rowCount; ++i)
      {
        for (int j = 0; j < temp.colCount; ++j)
        {
          double sum =0 ;
          for (int k = 0; k < lmat.colCount; ++k)
          {
            sum += lmat[i, k] * rmat[k, j];
          }
          temp[i, j] = sum;
        }
      }
      return temp;
    }
    
    #endregion

    #region Functions
    public void Show(int precision = 2)
    {
      for (int i = 0; i < rowCount; i++)
      {
        for (int j = 0; j < colCount; j++)
        {
          Console.Write($"{Math.Round(matrix[i, j], precision)}\t");
        }
        Console.WriteLine();
      }
    }
    public void GetTranspositioned(out Matrix matrix)
    {
      Matrix temp = new Matrix(this.colCount, this.rowCount);
      for (int i = 0; i < temp.rowCount; i++)
      {
        for (int j = 0; j < temp.colCount; j++)
        {
          temp[i, j] = this[j, i];
        }
      }
      matrix=temp;
    }
    public Matrix GetTranspositioned()
    {
      Matrix temp = new Matrix(this.colCount, this.rowCount);
      for (int i = 0; i < temp.rowCount; i++)
      {
        for (int j = 0; j < temp.colCount; j++)
        {
          temp[i, j] = this[j, i];
        }
      }
      return temp;
    }

    public void Transposition()
    {
      Matrix temp = new Matrix(this.colCount, this.rowCount);
      for (int i = 0; i < temp.rowCount; i++)
      {
        for (int j = 0; j < temp.colCount; j++)
        {
          temp[i, j] = this[j, i];
        }
      }

      this.matrix = new double[temp.rowCount, temp.colCount];
      this.rowCount = temp.rowCount;
      this.colCount = temp.colCount;
      this.matrix = temp.matrix;
    }

    private void SetValue(int row, int col, double value)
    {
      matrix[row, col] = value;
    }
    private void SetDet()
    {
      throw new NotImplementedException();
    }
    #endregion
  }
}
