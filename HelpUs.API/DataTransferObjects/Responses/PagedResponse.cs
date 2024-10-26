﻿using HelpUs.DataAccess.DataTransferObjects.Responses;

namespace HelpUs.API.DataTransferObjects.Responses
{
    public class PagedResponse<T> : APIResponse<T> where T : class
    {
        public PagedResponse()
        {

            Result = new List<T>();
        }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; } 

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public ICollection<T> Result { get; set; }
        public int PreviousPage
        {
            get
            {
                return CurrentPage == 1 ? 1 : CurrentPage - 1;
            }
        }

        public int NextPage
        {
            get
            {
                return CurrentPage == TotalPages ? TotalPages : CurrentPage + 1;
            }
        }


    }
}
