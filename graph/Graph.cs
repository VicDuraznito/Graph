using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    internal class Graph
    {
        public Vertex root { get; set; }
        public List<Vertex> vertexes = new List<Vertex>();

        public void insert(Vertex vertex, Vertex onVertex, int wei)
        {
            if (root == null)
            {
                root = vertex;
                vertexes.Add(vertex);
            }
            else
            {
                Edge edge1 = new Edge();
                edge1.weight = wei;
                edge1.vertexIni = onVertex;
                edge1.vertexEnd = vertex;
                onVertex.edges.Add(edge1);

                Edge edge2 = new Edge();
                edge2.weight = wei;
                edge2.vertexIni = vertex;
                edge2.vertexEnd = onVertex;
                vertex.edges.Add(edge2);

                onVertex.vertexes.Add(vertex);
                vertex.vertexes.Add(onVertex);

                int x = 0;
                foreach (Vertex v in vertexes)
                {
                    if (v.data.Equals(vertex.data))
                    {
                        x += 1;
                    }
                }
                if (x == 0)
                {
                    vertexes.Add(vertex);
                }
            }
        }
        public void delete(string data)
        {
            //foreach (Vertex v in vertexes)
            //{
            //    if (v.data.Equals(data))
            //    {
            //        v.vertexes.Clear();
            //        v.edges.Clear();
            //        vertexes.Remove(v);
            //    }
            //}

            for (int i = 0; i < vertexes.Count; i++)
            {
                if (vertexes[i].data.Equals(data))
                {
                    vertexes[i].vertexes.Clear();
                    vertexes[i].edges.Clear();
                    vertexes.Remove(vertexes[i]);
                }
            }
            foreach (Vertex v in vertexes)
            {
                foreach (Edge e in v.edges)
                {
                    if (e.vertexEnd.data.Equals(data))
                    {
                        v.edges.Remove(e);
                        break;
                    }
                }
            }
        }

        public void search(string data)
        {
            foreach (Vertex v in vertexes)
            {
                if (v.data.Equals(data))
                {
                    Console.WriteLine("Value found: " + v + " " + v.data);
                }
            }
            foreach (Vertex v in vertexes)
            {
                foreach (Edge e in v.edges)
                {
                    if (e.vertexIni.data.Equals(data) | e.vertexEnd.data.Equals(data))
                    {
                        Console.WriteLine("Edge: " + e.vertexIni.data + " " + e.vertexEnd.data);
                    }
                }
            }
        }
        public void printVertexes()
        {
            foreach (Vertex v in vertexes)
            {
                Console.WriteLine(v.data);
            }
        }


        public void matrix()
        {
            Console.Write(" ");
            for (int i = 0; i < vertexes.Count(); i++)
            {
                Console.Write(" " + vertexes[i].data);
            }
            for (int i = 0; i < vertexes.Count(); i++)
            {
                Console.WriteLine(" ");
                Console.Write(vertexes[i].data);
                for (int j = 0; j < vertexes.Count(); j++)
                {
                    //Console.WriteLine(vertexes[i].data + " " + vertexes[j].data);

                    if (vertexes[j].vertexes.Contains(vertexes[i]))
                    {
                        //Console.Write(vertexes[j].data);
                        //Console.WriteLine(" ");
                        Console.Write(" 1");
                    }
                    else
                    {
                        //Console.Write(vertexes[j].data);
                        //Console.WriteLine(" ");
                        Console.Write(" 0");
                    }
                }
            }
        }
        public void bfs(Vertex vertex)
        {

            List<Vertex> visited = new List<Vertex>();
            LinkedList<Vertex> queue = new LinkedList<Vertex>();
            visited.Add(vertex);
            queue.AddLast(vertex);

            while (queue.Count != 0)
            {
                vertex = queue.First();
                Console.WriteLine("next-> " + vertex.data);
                queue.RemoveFirst();

                foreach (Vertex v in vertex.vertexes)
                {
                    if (!visited.Contains(v))
                    {
                        visited.Add(v);
                        queue.AddLast(v);
                    }
                }
            }

        }
        public void dfs(Vertex vertex)
        {
            List<Vertex> visited = new List<Vertex>();
            Stack<Vertex> stack = new Stack<Vertex>();
            visited.Add(vertex);
            stack.Push(vertex);

            while (stack.Count != 0)
            {
                vertex = stack.Pop();
                Console.WriteLine("next-> " + vertex.data);
                foreach (Vertex v in vertex.vertexes)
                {
                    if (!visited.Contains(v))
                    {
                        visited.Add(v);
                        stack.Push(v);
                    }
                }
            }
        }

        public void shortestPath(Vertex vertex, Vertex iniVertex)
        {
            List<Vertex> path = new List<Vertex>();
            Stack<Vertex> stack = new Stack<Vertex>();

            path.Add(iniVertex);

            string d = vertex.data;
            Vertex vi = vertex;

            foreach (Vertex v1 in vertexes)
            {
                foreach (Vertex v2 in v1.vertexes)
                {
                    if (v2.data.Equals(vertex.data))
                    {
                        if (!stack.Contains(v1) && !path.Contains(v1))
                        {
                            //Console.WriteLine(v1.data);
                            stack.Push(v1);
                            path.Add(v1);
                        }
                    }
                }
            }
            foreach (Vertex s in stack)
            {
                Console.WriteLine(s.data);
            }
            Console.WriteLine(" ");
            int c = 0;
            Vertex pre = new Vertex();

            while (stack.Count != 0)
            {

                vertex = stack.Pop();
                Console.WriteLine("Pre: " + pre.data);
                foreach (Vertex v in vertex.vertexes)
                {

                    Console.WriteLine("V: " + v.data);
                    if (c == 0)
                    {
                        if (!v.data.Equals(d) && !path.Contains(v) && !v.Equals(iniVertex))
                        {
                            c += 1;
                            pre = v;
                            stack.Push(v);
                            path.Add(v);
                        }
                    }
                    else
                    {
                        if (!v.data.Equals(d) && !path.Contains(v) && !v.Equals(iniVertex) && vertex.vertexes.Contains(pre))
                        {
                            c += 1;
                            pre = v;
                            stack.Push(v);
                            path.Add(v);
                        }
                    }
                }
            }

            foreach (Vertex v in path)
            {
                Console.WriteLine(v.data);
            }
            //while (stack.Count != 0)
            //{
            //    vertex = stack.Pop();
            //    Console.WriteLine("next-> " + vertex.data);
            //    foreach (Vertex v in vertex.vertexes)
            //    {
            //        if (!path.Contains(v) && v.data.Equals(vertex.data))
            //        {
            //            path.Add(v);
            //            stack.Push(v);
            //        }
            //    }
            //}

        }
    }
}
