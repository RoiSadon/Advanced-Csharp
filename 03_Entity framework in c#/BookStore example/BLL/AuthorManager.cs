using System;
using System.Collections.Generic;
using BOL;
using DAL;

namespace BLL
{
    public class AuthorManager
    {
        public static List<AuthorModel> GetAllAuthors()
        {
            List<AuthorModel> authors = new List<AuthorModel>();

            using (BookStoreEntities ef = new BookStoreEntities())
            {
                foreach (Author item in ef.Authors)
                {
                    authors.Add(new AuthorModel
                    {
                        AuthorName = item.AuthorName,
                        AuthorAge = item.AuthorAge,
                        AuthorImage = item.AuthorImage
                    });
                }
            }
            return authors;
        }
    }

}