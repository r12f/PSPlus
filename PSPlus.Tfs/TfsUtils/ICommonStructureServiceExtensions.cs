using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.TeamFoundation.Server;

namespace PSPlus.Tfs.TfsUtils
{
    public static class ICommonStructureServiceExtensions
    {
        public static IEnumerable<NodeInfo> GetRoots(this ICommonStructureService css, string projectUri)
        {
            return css.ListStructures(projectUri);
        }

        public static NodeInfo GetNodeByPath(this ICommonStructureService css, string path)
        {
            NodeInfo nodeInfo = null;
            try
            {
                nodeInfo = css.GetNodeFromPath(path);
            }
            catch
            {
                nodeInfo = null;
            }

            return nodeInfo;
        }

        public static IEnumerable<NodeInfo> GetChildNodesByPath(this ICommonStructureService css, string path)
        {
            NodeInfo node = css.GetNodeByPath(path);
            if (node == null)
            {
                throw new ArgumentException(string.Format("Invalid path doesn't exist: {0}.", path));
            }

            XmlElement nodeXml = css.GetNodesXml(new string[] { node.Uri }, true);
            if (nodeXml.ChildNodes == null || nodeXml.ChildNodes.Count == 0)
            {
                yield break;
            }

            XmlNode childNodeRoot = nodeXml.ChildNodes[0];
            if (childNodeRoot.ChildNodes == null || childNodeRoot.ChildNodes.Count == 0)
            {
                yield break;
            }

            foreach (XmlNode childNode in childNodeRoot.ChildNodes)
            {
                XmlElement childElement = childNode as XmlElement;
                string childNodePath = childElement.GetAttribute("Path");
                yield return css.GetNodeByPath(childNodePath);
            }
        }

        public static IEnumerable<NodeInfo> MatchNodesByPath(this ICommonStructureService css, string path)
        {
            string parentPath = Path.GetDirectoryName(path);
            string nodeNamePattern = Path.GetFileName(path);
            return css.MatchChildNodesUnderPath(parentPath, nodeNamePattern);
        }

        public static IEnumerable<NodeInfo> MatchChildNodesUnderPath(this ICommonStructureService css, string parentPath, string nodeNamePattern)
        {
            WildcardPattern parsedNodeNamePattern = new WildcardPattern(nodeNamePattern, WildcardOptions.Compiled | WildcardOptions.CultureInvariant | WildcardOptions.IgnoreCase);

            IEnumerable<NodeInfo> nodes = css.GetChildNodesByPath(parentPath);
            foreach (var node in nodes)
            {
                if (parsedNodeNamePattern.IsMatch(node.Name))
                {
                    yield return node;
                }
            }
        }

        public static NodeInfo CreateNodeRecursively(this ICommonStructureService css, NodeInfo rootNode, string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return rootNode;
            }

            string path = string.Format("{0}\\{1}", rootNode.Path, relativePath);
            NodeInfo node = css.GetNodeByPath(path);
            if (node != null)
            {
                return node;
            }

            string parentRelativePath = Path.GetDirectoryName(relativePath);
            NodeInfo parentNode = css.CreateNodeRecursively(rootNode, parentRelativePath);

            string nodeName = Path.GetFileName(relativePath);
            string nodeUri = css.CreateNode(nodeName, parentNode.Uri);
            if (string.IsNullOrEmpty(nodeUri))
            {
                throw new InvalidOperationException(string.Format("Creating node failed: {0}.", path));
            }

            return css.GetNode(nodeUri);
        }
    }
}
