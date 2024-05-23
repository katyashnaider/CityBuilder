using System.Text;
using UnityEngine;

namespace CityBuilder.UI
{
    public static class TransformExtensions
    {
        private static readonly StringBuilder _pathBuilder = new StringBuilder();

        public static string GetFullPath(this Transform transform)
        {
            _pathBuilder.Clear();
            var buildFullPath = BuildFullPath(transform, _pathBuilder);
            var resultPath = buildFullPath.ToString();
            return resultPath;
        }

        public static StringBuilder BuildFullPath(Transform transform, StringBuilder pathBuilder)
        {
            if (transform.parent == null)
            {
                pathBuilder.Append($"/{transform.name}");
            }
            else
            {
                pathBuilder = BuildFullPath(transform.parent, pathBuilder).Append($"/{transform.name}");
            }

            return pathBuilder;
        }
    }
}