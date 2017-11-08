using PhotoAssistant.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace PhotoAssistant.Core {
    public static class TagNodeReverseCalculator {
        public static void ReverseTagNodesTree(DmContext context) {
            IEnumerable<DmTagNode> nodes = context.TagNodes.Local.Where(t => t.Parent == null);
            List<DmTagNodeReversed> rootNodes = new List<DmTagNodeReversed>();
            foreach(DmTagNode node in nodes) {
                List<DmTagNodeReversed> list = ConntectNodeToChildren(context, node);
                rootNodes.AddRange(list);
            }
            rootNodes = context.TagNodesReversed.Local.Where(t => t.Parent == null).ToList();
            GroupNodes(context, rootNodes);
        }
        static List<DmTagNodeReversed> ConntectNodeToChildren(DmContext context, DmTagNode node) => GetParentNodes(context, node);
        static void GroupNodes(DmContext context, List<DmTagNodeReversed> rootNodes) {
            if(rootNodes == null) {
                return;
            }

            List<DmTagNodeReversed> groupedNodes = new List<DmTagNodeReversed>();
            foreach(DmTagNodeReversed rev in rootNodes) {
                DmTagNodeReversed originNode = rootNodes.FirstOrDefault(n => n.Tag == rev.Tag);
                if(originNode == rev) {
                    groupedNodes.Add(rev);
                    continue;
                }
                List<DmTagNodeReversed> children = rev.Children.ToList();
                foreach(DmTagNodeReversed child in children) {
                    child.Parent = originNode;
                }
                context.TagNodesReversed.Remove(rev);
            }
            foreach(DmTagNodeReversed groupedNode in groupedNodes) {
                GroupNodes(context, groupedNode.Children == null ? null : groupedNode.Children.ToList());
            }
        }
        static List<DmTagNodeReversed> GetParentNodes(DmContext context, DmTagNode node) {
            List<DmTagNodeReversed> res = new List<DmTagNodeReversed>();
            if(node.Children == null || node.Children.Count == 0) {
                res.Add(new DmTagNodeReversed() { Tag = node.Tag, Type = node.Type });
                context.TagNodesReversed.Add(res[0]);
                return res;
            }
            foreach(DmTagNode child in node.Children) {
                List<DmTagNodeReversed> list = GetParentNodes(context, child);
                foreach(DmTagNodeReversed rchild in list) {
                    DmTagNodeReversed rnode = new DmTagNodeReversed() { Tag = node.Tag, Type = node.Type, Parent = rchild };
                    context.TagNodesReversed.Add(rnode);
                    res.Add(rnode);
                }
            }
            return res;
        }
    }
}
