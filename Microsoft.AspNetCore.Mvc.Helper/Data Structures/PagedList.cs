using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.AspNetCore.Mvc.Helper
{
    public class PagedList<T> : List<T>
    {
        #region Public Properties
        public int CurrentPage { get; private set; }

        public int TotalPages { get; private set; }

        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public bool HasPreviousPage {
            get {
                return CurrentPage > 1 && CurrentPage <= TotalPages;
            }
        }

        public bool HasNextPage {
            get {
                return CurrentPage < TotalPages;
            }
        }

        public int TotalItems { get; private set; }
        #endregion

        #region Constructors
        public PagedList(List<T> items, int count, int pageSize, int pageNumber) {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling((decimal)count / (decimal)pageSize);
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = count;
            AddRange(items);
        }
        #endregion

        #region Static Methods
        public static PagedList<T> Create(IQueryable<T> source, int pageSize, int pageNumber) {
            if (source == null || pageSize <= 0 || pageNumber <= 0)
                throw new ArgumentException();

            int count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageSize, pageNumber);
        }
        #endregion
    }
}
