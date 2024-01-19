using System;
using System.Collections.Generic;
using System.Text;

namespace FishStick.Commands.Autocompletion
{
    public class Trie
    {
        public class Node
        {
            public Dictionary<char, Node> Children { get; set; }
            public bool EndOfItem { get; set; }
            public int Depth { get; set; }
            public (char, Node) Parent { get; set; }

            public Node(int depth = 0, bool isEndOfItem = false)
            {
                Children = new Dictionary<char, Node>();
                EndOfItem = isEndOfItem;
                Depth = depth;
                Parent = (' ' , null);
            }
        }

        private Node Root;

        public Trie()
        {
            Root = new Node();
        }

        public void Insert(string s)
        {
            Node currNode = LCPNode(s);

            if (currNode.Depth >= s.Length)
            {
                currNode.EndOfItem = true;
                return;
            }

            for (int i = currNode.Depth; i < s.Length; i++)
            {
                char ch = s[i];

                if (!currNode.Children.ContainsKey(ch))
                {
                    Node newNode = new Node(i + 1);
                    currNode.Children.Add(ch, newNode);
                    newNode.Parent = (ch, currNode);
                }
                currNode = currNode.Children[ch];
            }
            currNode.EndOfItem = true;
        }

        public void Delete(string s)
        {
            Node currNode = LCPNode(s);

            if (currNode.Depth != s.Length || currNode.EndOfItem == false)
            {
                //Console.WriteLine("DBG: Delete(" + s + ") - string not found in Trie");
                return;
            }

            if (currNode.Children.Count > 0)
            {
                currNode.EndOfItem = false;
                return;
            }
            else
            {
                (char, Node) parent = currNode.Parent;
                Node parentNode = parent.Item2;
                while (parentNode != null)
                {
                    //Remove current node/edge
                    if (parentNode.Children.ContainsKey(parent.Item1))
                    {
                        parentNode.Children.Remove(parent.Item1);
                    }

                    if (parentNode.EndOfItem == true || parentNode == Root)
                    {
                        return;
                    }

                    currNode = parentNode;
                    parent = currNode.Parent;
                    parentNode = parent.Item2;
                }
            }
        }

        // Returns the Node of where the longest common prefix of input string and trie records ends
        public Node LCPNode(string s)
        {
            Node currNode = Root;

            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];

                if (currNode.Children.ContainsKey(ch))
                {
                    currNode = currNode.Children[ch];
                }
                else
                {
                    break;
                }
            }

            return currNode;
        }

        // Returns the length of the longest common prefix of input string and trie records
        public int LCPLen(string s)
        {
            return LCPNode(s).Depth;
        }
        public int LCPLen(Node n)
        {
            return n.Depth;
        }

        // A really really crude BFS implementation to find and return the closest complete item starting from parameter node
        public string GetShortestCompleteItem(Node from)
        {
            if (from == null)            
                from = this.Root;

            Queue<(Node, string)> q = new Queue<(Node, string)>();
            q.Enqueue((from, ""));

            Node node;
            string nodeStr;
            (Node, string) qItem;

            while (q.Count > 0)
            {
                qItem = q.Dequeue();
                node = qItem.Item1;
                nodeStr = qItem.Item2;

                if (node.EndOfItem)
                    return nodeStr;

                foreach (KeyValuePair<char, Node> edge in node.Children)
                {
                    (Node, string) newQItem = (edge.Value, nodeStr + edge.Key);
                    q.Enqueue(newQItem);
                }
            }

            return String.Empty;
        }
    }
}
