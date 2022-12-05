using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRAFO
{
    class graph

    {
        int[,] mAdyacencia;
        int[] indegree;
        int nodos;

        public graph (int pnodos)
        {
            nodos = pnodos;

            mAdyacencia = new int[nodos,nodos];
            indegree = new int[nodos]; 
        }
        public void AdicionaArista (int pnodoInicio, int pnodoFinal)
        {
            mAdyacencia[pnodoInicio, pnodoFinal] = 1;
        }
        public void MuestraAdyacencia()
        {
            int n = 0;
            int m = 0;  
        }
    }
}
