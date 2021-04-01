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
            byte[] ListPhoto = entity.Photo.ToArray();
            
            return new PhotoListModel
            {
                Id = entity.Id,
                Photo = ListPhoto
            };
        }

        public static PhotoDetailModel MapPhotoEntityToDetailModel(PhotoEntity entity)
        {
            byte[] modelPhoto = entity.Photo.ToArray();

            return new PhotoDetailModel
            {
                Id = entity.Id,
                Photo = modelPhoto,
                Extension = entity.Extension,
                Size = entity.Size
            };
        }

        public static PhotoEntity MapPhotoDetailModelToEntity(PhotoDetailModel model)
        {
            byte[] entityPhoto = model.Photo.ToArray();

            return new PhotoEntity
            {
                Id = model.Id,
                Photo = entityPhoto,
                Extension = model.Extension,
                Size = model.Size
            };
        }
    }
}
