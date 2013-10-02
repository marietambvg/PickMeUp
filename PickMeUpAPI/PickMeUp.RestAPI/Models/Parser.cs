using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PickMeUp.Models;
using PickMeUp.RestAPI.Models;

namespace PickMeUp.RestAPI.Models
{
    public class Parser
    {
        public static Expression<Func<Category, CategoryModel>> FromCategory
        {
            get
            {
                return x => new CategoryModel()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Image = new ImageModel
                    {
                         ID=x.Image.ID,
                         Source=x.Image.Source
                    }

                };
            }
        }

        public static Expression<Func<Image, ImageModel>> FromImage
        {
            get
            {
                return x => new ImageModel()
                {
                    ID = x.ID,
                    Source=x.Source

                };
            }
        }

        //public static Expression<Func<Movie, MovieModel>> FromMovie
        //{
        //    get
        //    {
        //        return x => new MovieModel()
        //        {
        //            Id = x.Id,
        //            Title = x.Title
        //        };
        //    }
        //}

        //public static Expression<Func<Projection, ProjectionModel>> FromProjection
        //{
        //    get
        //    {
        //        return x => new ProjectionModel()
        //        {
        //            Id = x.Id,
        //            Time = x.ProjectionTime,
        //            Movie = new MovieModel()
        //            {
        //                Id = x.Movie.Id,
        //                Title = x.Movie.Title
        //            },
        //            Cinema = new CinemaModel()
        //            {
        //                Id = x.Cinema.Id,
        //                Name = x.Cinema.Name
        //            }
        //        };
        //    }
        //}

        //internal static MovieDetailsModel ToMovieDetails(Movie entity)
        //{
        //    return new MovieDetailsModel()
        //    {
        //        Id = entity.Id,
        //        Title = entity.Title,
        //        Description = entity.Description,
        //        Actors = entity.Actors.Select(a => a.Fullname),
        //        Projections = entity.Projections.AsQueryable().Select(Parser.FromProjection)
        //    };
        //}

        //internal static ProjectionDetailsModel ToProjectionDetails(Projection entity)
        //{
        //    return new ProjectionDetailsModel()
        //    {
        //        Id = entity.Id,
        //        Time = entity.ProjectionTime,
        //        Movie = new MovieModel()
        //        {
        //            Id = entity.Movie.Id,
        //            Title = entity.Movie.Title
        //        },
        //        Seats = (from seatEntity in entity.Seats
        //                 select new SeatModel()
        //                 {
        //                     Row = seatEntity.Row,
        //                     Column = seatEntity.Column,
        //                     Status = seatEntity.Status.Status
        //                 })
        //    };
        //}

        //internal static ReservationModel ToReservationModel(Reservation reservation)
        //{
        //    return new ReservationModel()
        //    {
        //        UserCode = reservation.UserCode
        //    };
        //}
    }
}
