using System;
using System.Collections.Generic;
using System.Collections;

namespace 二叉树输出每层最左侧结点
{

    class bNode
    {
        public bNode left { get; set; }
        public bNode right { get; set; }
        public int data { get; set; }

        bNode(int value)
        {
            left = null;
            right = null;
            data = value;
        }

        public void PrintBTreeLevelLeft()
        {
            //  准备工作
            Queue<bNode> q = new Queue<bNode>();
            ArrayList arrayList = new ArrayList();
            q.Enqueue(this);
            arrayList.Add("/");
            //  开始
            Function(1, q, arrayList);
        }

        void Function(int i, Queue<bNode> q, ArrayList arrayList)  //  第几层，目前队列
        {
            Queue<bNode> qq = new Queue<bNode>();
            while (q.Count != 0)
            {
                bNode node = q.Dequeue();
                arrayList.Add(node.data);
                if (node.left != null)
                {
                    qq.Enqueue(node.left);
                }
                if(node.right != null)
                {
                    qq.Enqueue(node.right);
                }
                
            }
            if(qq.Count != 0)
            {
                arrayList.Add("/");
                Function(i + 1, qq, arrayList);
            }
            else
            {
                string str = "[";
                for(int j = 0; j < arrayList.Count; j++)
                {
                    if(arrayList[j].ToString() == "/" && j+1<arrayList.Count)
                    {
                        str = str + arrayList[j + 1] + ",";
                    }
                }
                str = str.Substring(0, str.Length - 1);
                str = str + "]";
                Console.WriteLine(str);
            }

        }

        static void Main()
        {
            bNode root = new bNode(2);
            root.left = new bNode(11);
            root.right = new bNode(23);

            root.left.left = new bNode(10);
            root.left.right = new bNode(15);

            root.right.left = new bNode(7);
            root.right.right = new bNode(14);

            root.right.left.right = new bNode(12);

            root.right.left.right.left = new bNode(13);

            root.PrintBTreeLevelLeft();
        }
    }

}
