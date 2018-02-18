using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Mvc.Helper
{
    public abstract class BaseRepository<T, C> where C: DbContext
    {
        #region Properties
        protected C Context { get; private set; }
        protected IHttpContextAccessor HTTPAccessor { get; private set; }
        protected ClaimsPrincipal User {
            get {
                if (HTTPAccessor == null || HTTPAccessor.HttpContext == null)
                    return null;
                return HTTPAccessor.HttpContext.User;
            }
        }

        public string Username {
            get {
                if (User == null)
                    return string.Empty;
                Claim cUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (cUser == null)
                    return string.Empty;
                return cUser.Value;
            }
        }

        protected ILogger<T> Logger { get; private set; }
        #endregion

        #region Constructor
        protected BaseRepository(C context, IHttpContextAccessor httpContextAccessor, ILogger<T> logger)
        {
            Context = context;
            HTTPAccessor = httpContextAccessor;
            Logger = logger;
        }
        #endregion

        public virtual async Task<bool> Save() {
            int result = await Context.SaveChangesAsync();
            return result > 0;
        }

        protected void CreateFolder(string path) {
            if (string.IsNullOrEmpty(path) || Directory.Exists(path))
                return;

            Directory.CreateDirectory(path);
        }

        protected void DeleteFile(string path) {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return;
            File.Delete(path);
        }
    }
}
