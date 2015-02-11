using System;

namespace Cession
{
	public class BinaryTreeNode<T>
	{
		public T Data{ get; set;}
		public BinaryTreeNode<T> LeftChild{get;set;}
		public BinaryTreeNode<T> RightChild{get;set;}

		public BinaryTreeNode(){
		}

		public BinaryTreeNode(T data){
			Data = data;
		}

		public BinaryTreeNode(T data,BinaryTreeNode<T> leftChild,BinaryTreeNode<T> rightChild){
			Data = data;
			LeftChild = leftChild;
			RightChild = rightChild;
		}
	}
}

