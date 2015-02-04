using System;

namespace Cession
{
	public class BinaryTree<T>
	{
		public BinaryTreeNode<T> Root{ get; set;}

		public BinaryTree (BinaryTreeNode<T> root)
		{
			this.Root = root;
		}

		public BinaryTree(){
		}

		public BinaryTreeNode<T> Search(Func<BinaryTreeNode<T>,bool> predicate){
			return Search(Root,predicate);
		}

		private BinaryTreeNode<T> Search(BinaryTreeNode<T> node,Func<BinaryTreeNode<T>,bool> predicate){
			if (node == null)
				return null;

			if (predicate (node))
				return node;

			var leftResult = Search(node.LeftChild,predicate);
			if (leftResult != null)
				return leftResult;

			return Search (node.RightChild, predicate);
		}
	}
}

