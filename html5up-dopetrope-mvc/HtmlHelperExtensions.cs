using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace html5up_dopetrope_mvc
{
    using html5up_dopetrope_mvc.Models;
    using html5up_dopetrope_mvc.Repositories;
    using System.IO;
    using System.Web.Mvc;

    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// An HtmlHelper for Adaptive Images
        /// EX. @Html.ImgTag("~/content/images/logo.png", "Company Name").WithDensities(2, 3).WithSize(100, 40)
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="imagePath"></param>
        /// <param name="altText"></param>
        /// <returns></returns>
        public static ImgTag ImgTag(this HtmlHelper htmlHelper, string imagePath, string altText)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return new ImgTag(imagePath, altText, urlHelper.Content);
        }

        /// <summary>
        /// virtual path from physical path
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="targetDirectory"></param>
        /// <param name="targetDirectory"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        public static IEnumerable<ImgTag> GetImgTagsByRoomId(this HtmlHelper htmlHelper, string virtualPath, string physicalPath)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            // 檔案目錄不存在時，建立目錄
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            var directory = new DirectoryInfo(physicalPath);
            var files = directory.GetFiles().Select(s => new ImgTag(virtualPath +s.Name, "showImage", urlHelper.Content)).ToList();
            return files;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="takeCount"></param>
        /// <returns></returns>
        public static IEnumerable<TB_News> GetNewsItemsOrderByDesc(this HtmlHelper htmlHelper, int takeCount)
        {
            using (var db = new NewsRepository())
            {
                return db.Find<TB_News>(s => s.IsDelete == false)
                    .OrderByDescending(desc => desc.PostDateTime)
                    .Take(takeCount).ToList();
            }
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }



    public class ImgTag : IHtmlString
    {
        public readonly string _imagePath;
        public readonly Func<string, string> _mapVirtualPath;
        private readonly HashSet<int> _pixelDensities;
        private readonly IDictionary<string, string> _htmlAttributes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="altText"></param>
        /// <param name="mapVirtualPath"></param>
        public ImgTag(string imagePath, string altText, Func<string, string> mapVirtualPath)
        {
            _imagePath = imagePath;
            _mapVirtualPath = mapVirtualPath;

            _pixelDensities = new HashSet<int>();
            _htmlAttributes = new Dictionary<string, string>
        {
            { "src", mapVirtualPath(imagePath) },
            { "alt", altText } 
        };
        }

        public string ToHtmlString()
        {
            var imgTag = new TagBuilder("img");

            if (_pixelDensities.Any())
            {
                AddSrcsetAttribute(imgTag);
            }

            foreach (KeyValuePair<string, string> attribute in _htmlAttributes)
            {
                imgTag.Attributes[attribute.Key] = attribute.Value;
            }

            return imgTag.ToString(TagRenderMode.SelfClosing);
        }

        private void AddSrcsetAttribute(TagBuilder imgTag)
        {
            int densityIndex = _imagePath.LastIndexOf('.');

            IEnumerable<string> srcsetImagePaths =
                from density in _pixelDensities
                let densityX = density + "x"
                let highResImagePath = _imagePath.Insert(densityIndex, "@" + densityX)
                    + " " + densityX
                select _mapVirtualPath(highResImagePath);

            imgTag.Attributes["srcset"] = string.Join(", ", srcsetImagePaths);
        }

        public ImgTag WithDensities(params int[] densities)
        {
            foreach (int density in densities)
            {
                _pixelDensities.Add(density);
            }

            return this;
        }

        public ImgTag WithSize(int width, int? height = null)
        {
            _htmlAttributes["width"] = width.ToString();
            _htmlAttributes["height"] = (height ?? width).ToString();

            return this;
        }
    }
}