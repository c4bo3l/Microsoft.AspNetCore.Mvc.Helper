using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.AspNetCore.Mvc.Helper
{
    public abstract class BasePagingRepository<T, C> : BaseRepository<T, C> where C:DbContext
    {
        #region Properties
        protected IUrlHelper UrlHelper { get; private set; }
        #endregion

        #region Contructors
        protected BasePagingRepository(C context, IHttpContextAccessor httpContextAccessor
            , ILogger<T> logger, IUrlHelper urlHelper) : base(context, httpContextAccessor, logger)
        {
            UrlHelper = urlHelper;
        }
        #endregion

        #region Create Resource URI
        /// <summary>
        /// Generate paging metadata for X-Pagination
        /// </summary>
        /// <typeparam name="sourceType"></typeparam>
        /// <param name="actionAlias"></param>
        /// <param name="items"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public PagingMetadata CreateResourceUri<sourceType>(string actionAlias, 
                PagedList<sourceType> items, PagingParameters param) {
            if (items == null || param == null)
                throw new ArgumentException();

            string prevPageLink = items.HasPreviousPage ? CreateResourceUri(actionAlias, param, 
                Enums.ResourceUriType.PreviousPage) : string.Empty;
            string nextPageLink = items.HasNextPage ? CreateResourceUri(actionAlias, param,
                Enums.ResourceUriType.NextPage) : string.Empty;

            return new PagingMetadata
            {
                TotalItems = items.TotalItems,
                TotalPages = items.TotalPages,
                PageSize = items.PageSize,
                PageNumber = items.PageNumber,
                PreviousPageLink = prevPageLink,
                NextPageLink = nextPageLink
            };
        }

        protected string CreateResourceUri(string actionAlias, PagingParameters param, Enums.ResourceUriType resourceUriType)
        {
            if (param == null || string.IsNullOrEmpty(actionAlias) || UrlHelper == null)
                throw new ArgumentException();

            if (resourceUriType == Enums.ResourceUriType.PreviousPage)
                return UrlHelper.Link(actionAlias, new
                {
                    pageSize = param.PageSize,
                    pageNumber = param.PageNumber - 1
                });
            else if (resourceUriType == Enums.ResourceUriType.NextPage)
                return UrlHelper.Link(actionAlias, new
                {
                    pageSize = param.PageSize,
                    pageNumber = param.PageNumber + 1
                });
            return UrlHelper.Link(actionAlias, new
            {
                pageSize = param.PageSize,
                pageNumber = param.PageNumber
            });
        }
        #endregion
    }
}
