using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using Main;

namespace Metodos_Numericos.Algorithms
{
    class Pointf
    {
        public decimal X,Y;

        public Pointf(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }

    }

    class TrazadoresCubicos
    {
        private List<Pointf> _listaF= new List<Pointf>();
        private string FPO="FPOd", FPN="FPNd";
        private decimal FPOd, FPNd;

        public void SetExample()
        {
            AddPoint( 2, 5 );
            AddPoint( 4, 6 );
            AddPoint( 5, 9 );
            AddPoint( 8, 5 );
            AddPoint( 10, 4 );
            AddFPO( 1 );
            AddFPN( 0 );
        }

        public void Clear()
        {
            _listaF.Clear();
        }

        public void AddPoint(decimal x, decimal y)
        {
            _listaF.Add(new Pointf(x,y));
        }

        public void AddFPN(string fpn)
        {
            FPN = fpn;
        }

        public void AddFPN( decimal fpn )
        {
            FPNd = fpn;
        }

        public void AddFPO(string fpo)
        {
            FPO = fpo;
        }

        public void AddFPO( decimal fpo )
        {
            FPOd = fpo;
        }

        // TRAZADORES CUBICOS NATURALES.
        public string Solve()
        {
            int MaximoFilas = _listaF.Count;
            //Asignacion de dimensiones (a arreglos necesarios) para el proceso.

            //Matriz 
            decimal[,] Matrix = new decimal[MaximoFilas, 6];

            //Arreglo con diferencias entre valores de las (X's)
            decimal[] h = new decimal[MaximoFilas];

            decimal[] alfa = new decimal[MaximoFilas];
            decimal[] l = new decimal[MaximoFilas];
            decimal[] miu = new decimal[MaximoFilas];
            decimal[] z = new decimal[MaximoFilas];

            //Arreglos de los coeficientes
            decimal[] c = new decimal[MaximoFilas];
            decimal[] b = new decimal[MaximoFilas];
            decimal[] d = new decimal[MaximoFilas];

            //Entrada de valores (X,Y)
            for (int i = 0; i <= MaximoFilas - 1; i++)
            {
                //X
                Matrix[i, 0] = _listaF[i].X;
                //F(X) o Y
                Matrix[i, 1] = _listaF[i].Y;
            }

            //Determinacion de las distancias (PASO 1)
            for (int i1 = 0; i1 <= MaximoFilas - 2; i1++)
            {
                h[i1] = Matrix[i1 + 1, 0] - Matrix[i1, 0];
            }

            //PASO 2
            for (int i2 = 1; i2 <= MaximoFilas - 2; i2++)
            {
                alfa[i2] = ((3 / h[i2]) * (Matrix[i2 + 1, 1] - Matrix[i2, 1])) - ((3 / h[i2 - 1]) * (Matrix[i2, 1] - Matrix[i2 - 1, 1]));
            }

            //PASOS (3,4,5) ENCARGADOS DE LA SOLUCION DE UN SISTEMA LINEAL TRIDIAGONAL)

            //PASO 3
            l[0] = 1;
            miu[0] = 0;
            z[0] = 0;

            //PASO 4
            for (int i3 = 1; i3 <= MaximoFilas - 2; i3++)
            {
                l[i3] = 2 * (Matrix[i3 + 1, 0] - Matrix[i3 - 1, 0]) - h[i3 - 1] * miu[i3 - 1];
                miu[i3] = h[i3] / l[i3];
                z[i3] = (alfa[i3] - h[i3 - 1] * z[i3 - 1]) / l[i3];

            }

            //PASO 5
            l[MaximoFilas - 1] = 1;
            z[MaximoFilas - 1] = 0;
            c[MaximoFilas - 1] = 0;

            //PASO 6
            for (int j = MaximoFilas - 2; j >= 0; j--)
            {
                c[j] = z[j] - miu[j] * c[j + 1];
                b[j] = (Matrix[j + 1, 1] - Matrix[j, 1]) / h[j] - (h[j] * (c[j + 1] + 2 * c[j]) / 3);
                d[j] = ((c[j + 1] - c[j]) / (3 * h[j]));
            }

            string STC = "";
            //PASO 7 (Copiado de los valores de los coeficientes (A,B,C,D) a la matriz madre)
            for (int j2 = 0; j2 <= MaximoFilas - 2; j2++)
            {
                //Coeficientes
                //A
                Matrix[j2, 2] = Matrix[j2, 1];
                //B
                Matrix[j2, 3] = b[j2];
                //C
                Matrix[j2, 4] = c[j2];
                //D
                Matrix[j2, 5] = d[j2];
                Console.WriteLine("J={0}, A={1}, B={2}, C={3}, D{4}", j2, Matrix[j2, 2], Matrix[j2, 3], Matrix[j2, 4], Matrix[j2, 5]);
                STC += string.Format("J={0}, A={1}, B={2}, C={3}, D{4}", j2, Matrix[j2, 2], Matrix[j2, 3], Matrix[j2, 4],Matrix[j2, 5]) + "\n";
            }

            //Cadena que almacenará el sistema de trazas cúbicas.
            

            //Generacion de (S.E) - Trazadores cubicos naturales (Conjuntos de polinomios)
            //for (int j3 = 1; j3 <= MaximoFilas - 3; j3++)
            //{
            //    STC = STC + "n" + Matrix[j3, 2].ToString() + " + " + Matrix[j3, 3].ToString() + " (x " + "-" + Matrix[j3, 0].ToString() + ") " + " + " + Matrix[j3, 4].ToString() + "  (x " + "-" + Matrix[j3, 0].ToString() + ")^2  " + " + " + Matrix[j3, 5].ToString() + "  (x " + "-" + Matrix[j3, 0].ToString() + ")^3  " + " - [" + Matrix[j3, 0].ToString() + "," + Matrix[j3 + 1, 0].ToString() + "]" + "r";
            //}

            //Regresame la cadena con los trazadores cúbicos.
            return (STC);
        }

        public string SolveS()
        {
            int MaximoFilas = _listaF.Count;
            //Asignacion de dimensiones (a arreglos necesarios) para el proceso.

            //Matriz 
            decimal[,] Matrix = new decimal[ MaximoFilas, 6 ];

            //Arreglo con diferencias entre valores de las (X's)
            decimal[] h = new decimal[ MaximoFilas ];

            decimal[] alfa = new decimal[ MaximoFilas ];
            decimal[] l = new decimal[ MaximoFilas ];
            decimal[] miu = new decimal[ MaximoFilas ];
            decimal[] z = new decimal[ MaximoFilas ];

            //Arreglos de los coeficientes
            decimal[] c = new decimal[ MaximoFilas ];
            decimal[] b = new decimal[ MaximoFilas ];
            decimal[] d = new decimal[ MaximoFilas ];

            //Entrada de valores (X,Y)
            for ( int i = 0; i <= MaximoFilas - 1; i++ )
            {
                //X
                Matrix[ i, 0 ] = _listaF[ i ].X;
                //F(X) o Y
                Matrix[ i, 1 ] = _listaF[ i ].Y;
            }

            //Determinacion de las distancias (PASO 1)
            for ( int i1 = 0; i1 <= MaximoFilas - 2; i1++ )
            {
                h[ i1 ] = Matrix[ i1 + 1, 0 ] - Matrix[ i1, 0 ];
            }

            //PASO 2
            alfa[ 0 ] = (  3 * ( Matrix[ 1, 1 ] - Matrix[ 0, 1 ] )  / h[ 0 ] - 3 * eval( FPO, Matrix[ 0, 0 ] ) );
            alfa[ MaximoFilas - 1 ] = ( 3 * eval( FPN, Matrix[ MaximoFilas - 1, 0 ] ) - 3 * ( Matrix[ MaximoFilas - 1, 1 ] - Matrix[ MaximoFilas - 2, 1 ] ) / h[ MaximoFilas - 2 ] );
            
            //PASO 3
            for ( int i2 = 1; i2 <= MaximoFilas - 2; i2++ )
            {
                alfa[ i2 ] = ( ( 3 / h[ i2 ] ) * ( Matrix[ i2 + 1, 1 ] - Matrix[ i2, 1 ] ) ) - ( ( 3 / h[ i2 - 1 ] ) * ( Matrix[ i2, 1 ] - Matrix[ i2 - 1, 1 ] ) );
            }

            //PASOS (4,5,6) ENCARGADOS DE LA SOLUCION DE UN SISTEMA LINEAL TRIDIAGONAL)

            //PASO 4
            l[ 0 ] = 2*h[0];
            miu[ 0 ] = 0.5m;
            z[ 0 ] = alfa[0]/l[0];

            //PASO 5
            for ( int i3 = 1; i3 <= MaximoFilas - 2; i3++ )
            {
                l[ i3 ] = 2 * ( Matrix[ i3 + 1, 0 ] - Matrix[ i3 - 1, 0 ] ) - h[ i3 - 1 ] * miu[ i3 - 1 ];
                miu[ i3 ] = h[ i3 ] / l[ i3 ];
                z[ i3 ] = ( alfa[ i3 ] - h[ i3 - 1 ] * z[ i3 - 1 ] ) / l[ i3 ];

            }

            //PASO 6
            l[ MaximoFilas - 1 ] = h[MaximoFilas-2]*(2-miu[MaximoFilas-2]);
            z[ MaximoFilas - 1 ] = (alfa[MaximoFilas-1]-h[MaximoFilas-2]*z[MaximoFilas-2])/l[MaximoFilas-1];
            c[ MaximoFilas - 1 ] = z[MaximoFilas-1];

            //PASO 7
            for ( int j = MaximoFilas - 2; j >= 0; j-- )
            {
                c[ j ] = z[ j ] - miu[ j ] * c[ j + 1 ];
                b[ j ] = ( Matrix[ j + 1, 1 ] - Matrix[ j, 1 ] ) / h[ j ] - ( h[ j ] * ( c[ j + 1 ] + 2 * c[ j ] ) / 3 );
                d[ j ] = ( ( c[ j + 1 ] - c[ j ] ) / ( 3 * h[ j ] ) );
            }

            string STC = "";
            //PASO 7 (Copiado de los valores de los coeficientes (A,B,C,D) a la matriz madre)
            for ( int j2 = 0; j2 <= MaximoFilas - 2; j2++ )
            {
                //Coeficientes
                //A
                Matrix[ j2, 2 ] = Matrix[ j2, 1 ];
                //B
                Matrix[ j2, 3 ] = b[ j2 ];
                //C
                Matrix[ j2, 4 ] = c[ j2 ];
                //D
                Matrix[ j2, 5 ] = d[ j2 ];
                Console.WriteLine( "J={0}, A={1}, B={2}, C={3}, D{4}", j2, Matrix[ j2, 2 ], Matrix[ j2, 3 ], Matrix[ j2, 4 ], Matrix[ j2, 5 ] );
                STC += string.Format( "J={0}, A={1}, B={2}, C={3}, D{4}", j2, Matrix[ j2, 2 ], Matrix[ j2, 3 ], Matrix[ j2, 4 ], Matrix[ j2, 5 ] ) + "\n";
            }

            //Cadena que almacenará el sistema de trazas cúbicas.


            //Generacion de (S.E) - Trazadores cubicos naturales (Conjuntos de polinomios)
            //for (int j3 = 1; j3 <= MaximoFilas - 3; j3++)
            //{
            //    STC = STC + "n" + Matrix[j3, 2].ToString() + " + " + Matrix[j3, 3].ToString() + " (x " + "-" + Matrix[j3, 0].ToString() + ") " + " + " + Matrix[j3, 4].ToString() + "  (x " + "-" + Matrix[j3, 0].ToString() + ")^2  " + " + " + Matrix[j3, 5].ToString() + "  (x " + "-" + Matrix[j3, 0].ToString() + ")^3  " + " - [" + Matrix[j3, 0].ToString() + "," + Matrix[j3 + 1, 0].ToString() + "]" + "r";
            //}

            //Regresame la cadena con los trazadores cúbicos.
            return ( STC );
        }

        private decimal eval(string function, decimal x )
        {
            if (function.Equals("FPOd"))
                return FPOd;
            if ( function.Equals( "FPNd" ) )
                return FPNd;
            MathParser parser = new MathParser();
            parser.LocalVariables.Add( "x", ( decimal ) x );
            var result = parser.Parse( function );
            return ( decimal ) result;
        }
    }
    //========================================================= Metodo De Biseccion ========================================================
    public class MetodoDeBiseccion
    {

        double fn(string function, double x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (double)result;
        }



        public string MetodoBiseccion(string function, double a, double b, double Tol, int iterador)
        {

            if ((fn(function, a) < 0 && fn(function, b) < 0) || (fn(function, b) > 0 && fn(function, a) > 0) || a > b)
                return "El metodo fracaso";
            var FA = fn(function, a);
            var i = 1;
            while (i <= iterador)
            {
                double p = a + ((b - a) / 2);
                var FP = fn(function, p);
                //  Console.WriteLine("#"+(i)+"  A= "+a+"  B= "+b+"  P= "+p+"  F(p)= "+FP);
                if (FP == 0 || (b - a) / 2 < Tol)
                    return "P" + (i) + "= " + p;
                i++;
                if (FA * FP > 0)
                {
                    a = p;
                    FA = FP;
                }
                else
                    b = p;
            }
            return "El metodo fracaso despues de " + iterador + " iteraciones";
        }
    }

    //===================================================================== MULLER ===========================================================
    class Muller
    {

        public decimal X0, X1, X2, Tol, N;

        public string Function;

        public Muller()
        {

        }

        private decimal F(decimal x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", x);
            var result = parser.Parse(Function);
            return result;
        }

        public void AddExample()
        {
            Function = "16x^4 - 40x^3 + 5x^2 +20x + 6";
            Tol = 0.00001m;
            X0 = 0.5m;
            X1 = 1;
            X2 = 1.5m;
            N = 7;
        }

        public string Solve()
        {
            decimal x0 = X0, x1 = X1, x2 = X2, tol = Tol, n = N;
            decimal h1, h2, d1, d2, d, b, D, E, h, p = decimal.Zero, i;

            //(Paso 1)
            h1 = x1 - x0;

            h2 = x2 - x1;

            d1 = (F(x1) - F(x0)) / h1;

            d2 = (F(x2) - F(x1)) / h2;

            d = (d2 - d1) / (h2 + h1);

            i = 3;


            //(Paso 2)

            while (i <= n)
            {
                //(Paso 3)
                b = d2 + h2 * d;


                D = Sqrt(((b * b) - 4 * F(x2) * d));
                //(Paso 4)
                if (Math.Abs(b - D) < Math.Abs(b + D))
                    E = b + D;
                else
                    E = b - D;
                //(Paso 5)
                h = -2 * F(x2) / E;
                p = x2 + h;
                Console.WriteLine("{0} - {1}", i, p);
                //(Paso 6)
                if (tol > Math.Abs(h))
                {
                    break;
                }
                //(Paso 7)
                x0 = x1;
                x1 = x2;
                x2 = p;
                h1 = x1 - x0;
                h2 = x2 - x1;
                d1 = (F(x1) - F(x0)) / h1;
                d2 = (F(x2) - F(x1)) / h2;
                d = (d2 - d1) / (h2 + h1);
                i++;
            }

            if (i > n)
            {
                Console.WriteLine(@"El Procedimiento fallo despues de {0} operaciones", n);
                return "El Procedimiento fallo despues de {0} operaciones"+ n;
            }
            else
            {
                Console.WriteLine("Solucion: {0} - {1}", i, p);
                return "Solucion: {0} - {1} " + i+ ", " +p;
            }


        }

        public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
        {
            if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

            decimal current = (decimal)Math.Sqrt((double)x), previous;
            do
            {
                previous = current;
                if (previous == 0.0M) return 0;
                current = (previous + x / previous) / 2;
            }
            while (Math.Abs(previous - current) > epsilon);
            return current;
        }

    }

    //=====================================================================================  METODO DE LA SECANTE ==============================================================

    class MetodoDeLaSecante
    {
        public decimal XO, Xl, EPS, EPSI, MAXIT;
        public string function;

        public MetodoDeLaSecante()
        {
            function = "x^3 + 2x^2 + 10x - 20";
            XO = 0;
            Xl = 1;
            EPS = 0;
            EPSI = (decimal)0.001;
            MAXIT = 4;
        }


        public string solve()
        {
            try
            {
                for (int i = 0; i < MAXIT; i++)
                {
                    var X = XO - (Xl - XO) * F(XO) / (F(Xl) - F(XO));
                    if (Math.Abs(X - Xl) < EPS)
                    {
                        Console.WriteLine(X);
                        return "La Respuesta Correcta es " + X.ToString();
                    }
                    if (Math.Abs(F(X)) < EPSI)
                    {
                        Console.WriteLine(X);
                        return "La Respuesta Correcta es " + X.ToString();
                    }
                    XO = Xl;
                    Xl = X;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"Error: " + e.Message);
            }
            Console.WriteLine(@"EL MÉTODO NO CONVERGE A UNA RAÍZ");
            return ("EL MÉTODO NO CONVERGE A UNA RAÍZ");
            

        }

        private decimal F(decimal x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (decimal)result;
        }
    }


    //============================================================INTERPOLACION DE LAGRANGE=============================

    public class InterpolacionLaGrange
    {

        public string Calculate(double x, double[] xd, double[] yd)
        {


            double sum = 0;
            for (int i = 0, n = xd.Length; i < n; i++)
            {
                if (Math.Abs(x - xd[i]) < 0)
                {
                    return yd[i].ToString(CultureInfo.InvariantCulture);
                }
                double FXINT = yd[i];
                for (int j = 0; j < n; j++)
                {
                    if ((i == j) || (Math.Abs(xd[i] - xd[j]) < 0))
                    {
                        continue;
                    }
                    FXINT *= (x - xd[j]) / (xd[i] - xd[j]);
                }
                sum += FXINT;
            }

            return "La Respuesta Correcta es: " + sum.ToString(CultureInfo.InvariantCulture);

        }
    }


    //================================================================INTERPOLANTE DE NEWTON=============================

    public class InterpolanteDeNewton
    {
        public string Calculate(double value, double[] x, double[] y)
        {
            var b = new double[x.Length, y.Length];

            for (var i = 0; i < x.Length; i++)
                b[i, 0] = y[i];

            for (var j = 1; j < x.Length; j++)
                for (var i = 0; i < x.Length - j; i++)
                    b[i, j] = (b[i + 1, j - 1] - b[i, j - 1]) / (x[i + j] - x[i]);

            double xt = 1;
            double yi = b[0, 0];
            for (var i = 0; i < x.Length - 1; i++)
            {
                xt = xt * (value - x[i]);
                yi = yi + b[0, i + 1] * xt;

            }

            return "La respuesta Correcta es:" + yi;
        }
    }


    //=========================================================NEWTON-RAPHSON==========================================

    public class NewtonRaphson
    {
        double fn(string function, double x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (double)result;
        }


        public string newtonRaphson(string function, string function2, double aprox, double TOL, double iterador)
        {

            int i = 1;
            while (i <= iterador)
            {
                double p = aprox - (fn(function, aprox) / fn(function2, aprox));
                //Console.WriteLine("#" + (i) + "  Po= " + aprox + "  P= " + p);
                if (Math.Abs(p - aprox) < TOL)
                    return "La Respuesta Correcta es P" + (i) + "= " + p;
                i++;
                aprox = p;
            }
            return "El metodo fracaso despues de " + iterador + " iteraciones";
        }
    }

    //==================================================FALSA POSICION==========================================================

    public class PosicionFalsa
    {
        double Fn(string function, double x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (double)result;
        }
        public string Calculate(string function, double p0, double p1, double tol, int iterador)
        {
            var q0 = Fn(function, p0);
            var q1 = Fn(function, p1);
            var i = 2;
            for (; i <= iterador; i++)
            {
                var processPartialResult = p1 - (q1 * (p1 - p0) / (q1 - q0));

                if (Math.Abs(processPartialResult - p1) < tol)
                {
                    return "La Respuesta Correcta es " + processPartialResult;
                }

                var qProcessPartialResult = Fn(function, processPartialResult);

                if ((qProcessPartialResult * q1) < 0)
                {
                    p0 = p1;
                    q0 = q1;
                }

                p1 = processPartialResult;
                q1 = qProcessPartialResult;


            }
            return "Processo falló en la iteración: " + i;
        }
    }


    //==================================================================PUNTO FIJO========================================

    public class PuntoFijo
    {
        double fn(string function, double x)
        {
            MathParser parser = new MathParser();
            parser.LocalVariables.Add("x", (decimal)x);
            var result = parser.Parse(function);
            return (double)result;
        }
        public string puntofijo(string function, string function2, double aprox, double TOL, double iterador)
        {
            int i = 1;
            while (i <= iterador)
            {
                var p = fn(function2, aprox);
                if (Math.Abs(p - aprox) < TOL)
                    return "La Respuesta Correcta es P" + (i) + "= " + p;
                i++;
                aprox = p;
            }
            return "El metodo fracaso despues de " + iterador + " iteraciones";
        }
    }

}
