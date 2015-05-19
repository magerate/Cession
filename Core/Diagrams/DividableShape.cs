using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Cession.Geometries;

namespace Cession.Diagrams
{
    public class DividableShape:CompositeShape
    {
        class DivideData
        {
            public Polyline Divider{ get; set; }
            public Path Path{ get; set; }
        }

        private Path _contour;

        private BinaryTree<DivideData> _dividerTree;

        public DividableShape (Path contour)
        {
            _contour = contour;
        }

        public override IEnumerator<Shape> GetEnumerator ()
        {
            yield return _contour;
        }

        public void AddDivider (Path path, Polyline divider)
        {
            var node = GetTreeNode (path);
            node.Data.Divider = divider;

            var leftChild = new BinaryTreeNode<DivideData> (new DivideData (){ Path = null });
            var rightChild = new BinaryTreeNode<DivideData> (new DivideData (){ Path = null });

            node.LeftChild = leftChild;
            node.RightChild = rightChild;
        }

        public void RemoveDivider (Path path, Polyline divider)
        {
            var node = GetTreeNode (path);
            node.Data.Divider = null;

            node.LeftChild = null;
            node.RightChild = null;
        }

        private BinaryTreeNode<DivideData> GetTreeNode (Path path)
        {
            if (null == _dividerTree) {
                _dividerTree = new BinaryTree<DivideData> ();
                var data = new DivideData (){ Path = _contour };
                _dividerTree.Root = new BinaryTreeNode<DivideData> (data);
            }

            return _dividerTree.Search (n => n.Data.Path == path);
        }
    }
}

