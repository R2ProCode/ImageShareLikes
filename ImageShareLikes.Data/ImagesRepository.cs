using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageShareLikes.Data
{
    public class ImagesRepository
    {
        private readonly string _connectionString;
        public ImagesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Image> GetImages()
        {
            using var ctx = new ImageDataContext(_connectionString);
            return ctx.Images.OrderByDescending(i => i.DateCreated).ToList();
        }

        public void AddImage(Image image)
        {
            using var ctx = new ImageDataContext(_connectionString);
            ctx.Images.Add(image);
            ctx.SaveChanges();
        }

        public Image GetImageById(int id)
        {
            using var ctx = new ImageDataContext(_connectionString);
            return ctx.Images.FirstOrDefault(i => i.Id == id);
        }

        public void UpdateImageLikes(int id)
        {
            using var ctx = new ImageDataContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"UPDATE Images SET Likes = Likes +1 WHERE Id = {id}");
        }

        public int GetImageLikes(int id)
        {
            using var ctx = new ImageDataContext(_connectionString);
            return ctx.Images.FirstOrDefault(i => i.Id == id).Likes;
        }
    }
}
