using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;
using ThinkInBio.CommonApp.BLL;
using R = ThinkInBio.CommonApp.WSL.Properties.Resources;

namespace ThinkInBio.CommonApp.WSL.Impl
{
    public class NoticeWcfService : INoticeWcfService
    {

        internal INoticeService NoticeService { get; set; }
        internal IExceptionHandler ExceptionHandler { get; set; }

        public Notice SaveNotice(string title, string content, string creator)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new WebFaultException<string>(R.EmptyName, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(creator))
            {
                throw new WebFaultException<string>(R.EmptyUser, HttpStatusCode.BadRequest);
            }

            try
            {
                Notice notice = new Notice();
                notice.Title = title;
                notice.Content = content;
                notice.Creator = creator;
                notice.Save((e) =>
                {
                    NoticeService.SaveNotice(e);
                });
                return notice;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public Notice UpdateNotice(string id, string title, string content, string creator)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new WebFaultException<string>(R.EmptyName, HttpStatusCode.BadRequest);
            }
            if (string.IsNullOrWhiteSpace(creator))
            {
                throw new WebFaultException<string>(R.EmptyUser, HttpStatusCode.BadRequest);
            }
            long idLong = 0;
            try
            {
                idLong = Convert.ToInt64(id);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }

            try
            {
                Notice notice = NoticeService.GetNotice(idLong);
                if (notice == null)
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                if (creator != notice.Creator)
                {
                    throw new WebFaultException(HttpStatusCode.Forbidden);
                }
                notice.Title = title;
                notice.Content = content;
                notice.Update((e) =>
                {
                    NoticeService.UpdateNotice(e);
                });
                return notice;
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public void DeleteNotice(string id)
        {
            long idLong = 0;
            try
            {
                idLong = Convert.ToInt64(id);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }

            try
            {
                Notice notice = NoticeService.GetNotice(idLong);
                if (notice == null)
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                NoticeService.DeleteNotice(notice);
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public Notice GetNotice(string id)
        {
            long idLong = 0;
            try
            {
                idLong = Convert.ToInt64(id);
            }
            catch
            {
                throw new WebFaultException<string>(R.InvalidId, HttpStatusCode.BadRequest);
            }

            try
            {
                Notice notice = NoticeService.GetNotice(idLong);
                if (notice == null)
                {
                    throw new WebFaultException(HttpStatusCode.NotFound);
                }
                return notice;
            }
            catch (WebFaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public Notice[] GetNoticeList(string date, string span, string start, string count)
        {
            DateTime d = DateTime.MinValue;
            int spanInt = 0;
            if ("null" != date && "null" != span)
            {
                try
                {
                    d = DateTime.Parse(date);
                }
                catch
                {
                    throw new WebFaultException<string>("date", HttpStatusCode.BadRequest);
                }
                try
                {
                    spanInt = Convert.ToInt32(span);
                }
                catch
                {
                    throw new WebFaultException<string>("span", HttpStatusCode.BadRequest);
                }
            }

            int startInt = 0;
            try
            {
                startInt = Convert.ToInt32(start);
            }
            catch
            {
                throw new WebFaultException<string>("start", HttpStatusCode.BadRequest);
            }
            int countInt = 0;
            try
            {
                countInt = Convert.ToInt32(count);
            }
            catch
            {
                throw new WebFaultException<string>("count", HttpStatusCode.BadRequest);
            }

            DateTime? startTime = null;
            DateTime? endTime = null;
            if ("null" != date && "null" != span)
            {
                if (spanInt < 0)
                {
                    startTime = d.AddDays(spanInt + 1);
                    endTime = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
                }
                else
                {
                    startTime = new DateTime(d.Year, d.Month, d.Day);
                    endTime = d.AddDays(spanInt).AddSeconds(-1);
                }
            }

            try
            {
                IList<Notice> list = NoticeService.GetNoticeList(startTime, endTime, startInt, countInt);
                if (list != null)
                {
                    return list.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
