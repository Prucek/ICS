using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSproj.BL.Models;
using ICSproj.DAL.Entities;

namespace ICSproj.BL.Mappers
{
    public static class PhotoMapper
    {
        public static PhotoListModel MapPhotoEntityToListModel(PhotoEntity entity)
        {
            if (entity == null) return null;

            return new PhotoListModel
            {
                Id = entity.Id,
                Photo = entity.Photo
            };
        }

        public static PhotoDetailModel MapPhotoEntityToDetailModel(PhotoEntity entity)
        {
            if (entity == null) return null;

            return new PhotoDetailModel
            {
                Id = entity.Id,
                Photo = entity.Photo,
                Extension = entity.Extension,
            };
        }

        public static PhotoEntity MapPhotoDetailModelToEntity(PhotoDetailModel model)
        {
            return new PhotoEntity
            {
                Id = model.Id,
                Photo = model.Photo,
                Extension = model.Extension,
            };
        }
    }
}
