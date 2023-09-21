using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
//C:\Users\cadri\OneDrive\Escritorio\input.csv
namespace lab1EDII_CésarSilva
{
    public class HuffmanNode
    {
        public string Value { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }
    }

    class insertar
    {
        public insertar(string nombre, string dpis, string fechas, string direcciones, List<string> empresas)
        {
            name = nombre;
            dpi = dpis;
            datebirth = fechas;
            address = direcciones;
            companies = empresas;
        }
        public string name { get; set; }
        public string dpi { get; set; }
        public string datebirth { get; set; }
        public string address { get; set; }
        public List<string> companies { get; set; }
        static void GenerateHuffmanCodes(HuffmanNode node, string code, Dictionary<string, string> huffmanCodes)
        {
            if (node == null)
                return;

            if (node.Left == null && node.Right == null)
            {
                huffmanCodes[node.Value] = code;
            }

            GenerateHuffmanCodes(node.Left, code + "0", huffmanCodes);
            GenerateHuffmanCodes(node.Right, code + "1", huffmanCodes);
        }
        public Dictionary<string, string> CodificarHuffman()
        {
            Dictionary<string, int> frequencyDict = new Dictionary<string, int>();

            // Calcular la frecuencia de cada elemento en la lista companies
            foreach (var company in companies)
            {
                if (frequencyDict.ContainsKey(company))
                {
                    frequencyDict[company]++;
                }
                else
                {
                    frequencyDict[company] = 1;
                }
            }

            // Construir el árbol de codificación de Huffman
            List<HuffmanNode> nodes = frequencyDict.Select(pair => new HuffmanNode
            {
                Value = pair.Key,
                Frequency = pair.Value
            }).ToList();

            while (nodes.Count > 1)
            {
                nodes = nodes.OrderBy(node => node.Frequency).ToList();
                var left = nodes[0];
                var right = nodes[1];
                nodes.Remove(left);
                nodes.Remove(right);

                var parent = new HuffmanNode
                {
                    Value = left.Value + right.Value,
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };

                nodes.Add(parent);
            }

            // Obtener el código Huffman para cada elemento en la lista companies
            Dictionary<string, string> huffmanCodes = new Dictionary<string, string>();
            GenerateHuffmanCodes(nodes[0], "", huffmanCodes);

            return huffmanCodes;
        }

        public string DecodificarHuffmanEmpresas(Dictionary<string, string> huffmanCodes, string codigoHuffman)
        {
            // Crear un diccionario inverso para buscar los valores correspondientes a los códigos Huffman
            Dictionary<string, string> reverseHuffmanCodes = huffmanCodes.ToDictionary(pair => pair.Value, pair => pair.Key);

            string resultado = "";
            string codigoActual = "";

            foreach (char bit in codigoHuffman)
            {
                codigoActual += bit;
                if (reverseHuffmanCodes.ContainsKey(codigoActual))
                {
                    resultado += reverseHuffmanCodes[codigoActual];
                    codigoActual = "";
                }
            }

            return resultado;
        }
    }
    
    
    class serializar
    {
        public serializar(string nombre, string dpis, string fechas, string direcciones, List<string> empres)
        {
            name = nombre;
            dpi = dpis;
            datebirth = fechas;
            address = direcciones;
            companies = empres;
        }
        public string name { get; set; }
        public string dpi { get; set; }
        public string datebirth { get; set; }
        public string address { get; set; }
        public List<string> companies { get; set; }
        static void GenerateHuffmanCodes(HuffmanNode node, string code, Dictionary<string, string> huffmanCodes)
        {
            if (node == null)
                return;

            if (node.Left == null && node.Right == null)
            {
                huffmanCodes[node.Value] = code;
            }

            GenerateHuffmanCodes(node.Left, code + "0", huffmanCodes);
            GenerateHuffmanCodes(node.Right, code + "1", huffmanCodes);
        }
        public Dictionary<string, string> CodificarHuffmanEmpresas()
        {
            Dictionary<string, int> frequencyDict = new Dictionary<string, int>();

            foreach (var company in companies)
            {
                if (frequencyDict.ContainsKey(company))
                {
                    frequencyDict[company]++;
                }
                else
                {
                    frequencyDict[company] = 1;
                }
            }

            List<HuffmanNode> nodes = frequencyDict.Select(pair => new HuffmanNode
            {
                Value = pair.Key,
                Frequency = pair.Value
            }).ToList();

            while (nodes.Count > 1)
            {
                nodes = nodes.OrderBy(node => node.Frequency).ToList();
                var left = nodes[0];
                var right = nodes[1];
                nodes.Remove(left);
                nodes.Remove(right);

                var parent = new HuffmanNode
                {
                    Value = left.Value + right.Value,
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };

                nodes.Add(parent);
            }

            Dictionary<string, string> huffmanCodes = new Dictionary<string, string>();
            GenerateHuffmanCodes(nodes[0], "", huffmanCodes);

            return huffmanCodes;
        }

        public string DecodificarHuffmanEmpresas(Dictionary<string, string> huffmanCodes, string codigoHuffman)
        {
            // Crear un diccionario inverso para buscar los valores correspondientes a los códigos Huffman
            Dictionary<string, string> reverseHuffmanCodes = huffmanCodes.ToDictionary(pair => pair.Value, pair => pair.Key);

            string resultado = "";
            string codigoActual = "";

            foreach (char bit in codigoHuffman)
            {
                codigoActual += bit;
                if (reverseHuffmanCodes.ContainsKey(codigoActual))
                {
                    resultado += reverseHuffmanCodes[codigoActual];
                    codigoActual = "";
                }
            }

            return resultado;
        }
    }
    public class AVLNode
    {
        public int Data { get; set; }
        public int Height { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }

        public AVLNode(int data)
        {
            Data = data;
            Height = 1;
            Left = null;
            Right = null;
        }
    }

    public class AVLTree
    {
        private AVLNode root;

        public AVLTree()
        {
            root = null;        // Constructor
        }

        public void Insert(int data)
        {
            root = Insert(root, data);         // Insertar un nodo en el árbol
        }

        private AVLNode Insert(AVLNode node, int data)
        {
            if (node == null)
            {
                return new AVLNode(data);
            }

            if (data < node.Data)
            {
                node.Left = Insert(node.Left, data);
            }
            else if (data > node.Data)
            {
                node.Right = Insert(node.Right, data);
            }
            else // Duplicados no permitidos en este ejemplo
            {
                return node;
            }

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));// Actualizar la altura del nodo actual

            int balance = GetBalance(node);// Verificar el balance y realizar rotaciones si es necesario

            return node;
        }

        public void Delete(int data)
        {
            root = Delete(root, data);// Eliminar un nodo del árbol
        }

        private AVLNode Delete(AVLNode node, int data)
        {
            return null;
        }

        private int Height(AVLNode node)// Funciones de equilibrio y rotación
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }
        
        private int GetBalance(AVLNode node)
        {
            if (node == null)
            {
                return 0;
            }
            return Height(node.Left) - Height(node.Right);
        }
    }

    class lectura
    {
        public lectura(string ope, string js)
        {
            operacion = ope;
            jsond = js;
        }
        public string operacion { get; set; }
        public string jsond { get; set; }
    }
    class Program
    {
        
        public static List<serializar> personas = new List<serializar>();
        public static List<lectura> jtotal = new List<lectura>();
        public static List<serializar> inserte = new List<serializar>();
        public static List<serializar> delete = new List<serializar>();
        public static List<serializar> patch = new List<serializar>();
        public static List<serializar> resultados = new List<serializar>();
        public static Dictionary<string, string> huffmanCodes = new Dictionary<string, string>();
        public static AVLTree arbol = new AVLTree();
        static void Main(string[] args)
        {

            bool volver = true;
            string opi;
            while (volver == true)
            {
                try
                {
                    volver = false;
                    Console.Clear();
                    int opcion;
                    string ruta;
                    Console.WriteLine("----------------------------MENÚ bY CÉSAR SILVA / 1184519----------------------------");
                    Console.WriteLine("------------------------------Ingrese opcion a realizar------------------------------");
                    Console.WriteLine("1. Operaciones a travez de un nuevo CSV");
                    Console.WriteLine("2. Buscar un dato por DPI");
                    Console.WriteLine("3. Buscar un dato por nombre");
                    Console.WriteLine("4. Salir");
                    opcion = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch (opcion)
                    {
                        case 1: //Insertar valores por medio de csv
                            inserte.Clear();
                            Console.WriteLine("Inserte ruta absoluta del archivo");
                            ruta = Console.ReadLine();
                            string[] archivo = File.ReadAllLines(ruta);
                            foreach (var linea in archivo)
                            {
                                string[] valores = linea.Split(';'); //se divide el csv en 2 partes
                                lectura guardar = new(valores[0], valores[1]); //se llama a la lista guardar con los valores separados
                                jtotal.Add(guardar);//se agrega a la lista jtotal los valores guardados
                                                    //string cosa;
                                                    //cosa = JsonConvert.SerializeObject(lectura);

                                guardar.jsond = "[" + guardar.jsond + "]";//se compone el formato json que está en guardar
                                guardar.jsond = guardar.jsond.Replace("\"\"", "\"");
                                if (guardar.operacion == "INSERT")
                                {
                                    personas = JsonConvert.DeserializeObject<List<serializar>>(guardar.jsond);//se deserializa el json
                                    inserte.AddRange(personas);//se agregan los valores a la lista inserte
                                }
                                else if (guardar.operacion == "DELETE")
                                {
                                    personas = JsonConvert.DeserializeObject<List<serializar>>(guardar.jsond);//se deserializa el json
                                                                                                              //inserte.RemoveRange(personas);
                                    delete.AddRange(personas);
                                    foreach (var item in delete)
                                    {
                                        inserte.RemoveAll(inserte =>
                                                            inserte.name == item.name &&
                                                            inserte.dpi == item.dpi &&
                                                            inserte.address == item.address &&
                                                            inserte.datebirth == item.datebirth &&
                                                            inserte.companies == item.companies);
                                    }
                                    delete.Clear();
                                }
                                else if (guardar.operacion == "PATCH")
                                {
                                    personas = JsonConvert.DeserializeObject<List<serializar>>(guardar.jsond);//se deserializa el json
                                    patch.AddRange(personas);
                                    foreach (var item in patch)
                                    {
                                        var reemplazo = inserte.Find(inserte =>
                                                                                inserte.name == item.name &&
                                                                                inserte.dpi == item.dpi);
                                        if (item.address != null)
                                        {
                                            //reemplazo.datebirth = item.datebirth;
                                            reemplazo.address = item.address;
                                        }
                                        if (item.datebirth != null)
                                        {
                                            reemplazo.datebirth = item.datebirth;
                                            //reemplazo.address = item.address;
                                        }
                                        if (item.companies != null)
                                        {
                                            reemplazo.companies = item.companies;
                                        }
                                        //if (reemplazo != null)
                                        //{
                                        //    reemplazo.datebirth = item.datebirth;
                                        //    reemplazo.address = item.address;
                                        //}
                                    }
                                    patch.Clear();
                                }
                                foreach (var item in inserte)
                                {
                                    string datoo = Convert.ToString(item);
                                    Dictionary<string, string> huffmanCodes = item.CodificarHuffmanEmpresas();
                                }
                                //AVLTree arbol = new AVLTree();
                                foreach (var item in inserte)
                                {
                                    if (int.TryParse(item.dpi, out int value))
                                    {
                                        arbol.Insert(value);
                                    }
                                }
                            }
                            Console.Clear();
                            //foreach (var item in inserte)
                            //{
                            //    Console.WriteLine(item.name);
                            //    Console.WriteLine(item.dpi);
                            //    Console.WriteLine(item.datebirth);
                            //    Console.WriteLine(item.address);
                            //}
                            Console.WriteLine("----------------------------LISTO----------------------------");
                            Console.WriteLine("----------------------Lectura exitosa------------------------");
                            Console.WriteLine("-------------operaciones realizada correctamente-------------");
                            Console.WriteLine("-----------------Volviendo al menú principal-----------------");
                            volver = true;
                            Console.ReadLine();
                            break;
                        case 2: //BUSQUEDA DPI
                            resultados.Clear();
                            AVLTree arbol1;
                            Console.WriteLine("----------------------Que desea buscar----------------------");
                            string busqueda = Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("----------------------Sus Resultados----------------------");
                            foreach (var item in inserte)
                            {
                                serializar busca = personas.Find(p => p.dpi == busqueda);

                                if (item.dpi == busqueda)
                                {
                                    arbol1 = new AVLTree();
                                    Dictionary<string, string> huffmanCodes = item.CodificarHuffmanEmpresas();
                                    Console.WriteLine($"name: {item.name}, dpi: {item.dpi}, datebith: {item.datebirth}, address: {item.address}, Empresas:");
                                    foreach (var kvp in huffmanCodes)
                                    {
                                        Console.WriteLine($"{kvp.Value}");
                                    }
                                    resultados.Add(item);
                                }
                            }
                            Console.WriteLine("");
                            Console.ReadLine();
                            string jsonl = "resultados.jsonl";
                            using (StreamWriter escritura = new StreamWriter(jsonl))
                            {
                                foreach (var resul in resultados)//arbol)
                                {
                                    string empresaDecodificada = resul.DecodificarHuffmanEmpresas(huffmanCodes, Convert.ToString(resul.companies));
                                    string jsons = JsonConvert.SerializeObject(resul);
                                    escritura.WriteLine(jsons);
                                }
                            }
                            Console.WriteLine($"-Se guardaron exitosamente en un archivo llamado: { jsonl}-");
                            Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("-----------------¿Desea volver al menú principal?-----------------");
                            Console.WriteLine("---------Presione 1 para volver, u otra tecla para salir.---------");
                            opi = Console.ReadLine();
                            if (opi == "1")
                            {
                                volver = true;
                                Console.Clear();
                                Console.WriteLine("-----------------Volviendo al menú principal-----------------");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("----------------------------Adios----------------------------");
                                Console.ReadLine();
                            }
                            break; //
                        case 3: //BUSQUEDA NOMBRE
                            resultados.Clear();
                            AVLTree arbol12;
                            Console.WriteLine("----------------------Que desea buscar----------------------");
                            string busqueda2 = Console.ReadLine();
                            Console.Clear(); 
                            Console.WriteLine("----------------------Sus Resultados----------------------");
                            foreach (var item in inserte)
                            {
                                serializar busca = inserte.Find(p => p.name == busqueda2);
                                if (item.name == busqueda2)
                                {
                                    arbol12 = new AVLTree();
                                    Console.WriteLine($"name: {item.name}, dpi: {item.dpi}, datebith: {item.datebirth}, address: {item.address}");
                                    
                                    resultados.Add(item);
                                }
                            }
                            Console.WriteLine("");
                            Console.ReadLine();
                            string jsonl2 = "resultados.jsonl";
                            using (StreamWriter escritura = new StreamWriter(jsonl2))
                            {
                                foreach (var resul in resultados)//arbol)
                                {
                                    string jsons = JsonConvert.SerializeObject(resul);
                                    escritura.WriteLine(jsons);
                                }
                            }
                            Console.WriteLine($"-Se guardaron exitosamente en un archivo llamado: { jsonl2}-");
                            Console.ReadLine();
                            Console.Clear();
                            Console.WriteLine("-----------------¿Desea volver al menú principal?-----------------");
                            Console.WriteLine("---------Presione 1 para volver, u otra tecla para salir.---------");
                            opi = Console.ReadLine();
                            if (opi == "1")
                            {
                                volver = true;
                                Console.Clear();
                                Console.WriteLine("-----------------Volviendo al menú principal-----------------");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("----------------------------Adios----------------------------");
                                Console.ReadLine();
                            }
                            break;
                        case 4://SALIDA
                            Console.Clear();
                            Console.WriteLine("----------------------------Adios----------------------------");
                            Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("----------------------------Opcion invalida----------------------------");
                            Console.ReadLine();
                            volver = true;
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.Clear();
                    volver = true;
                    Console.WriteLine("---------------------------ERROR----------------------------");
                    Console.WriteLine("------------------------favor revisar-----------------------");
                    Console.ReadLine();
                }
            }
        }
    }
}