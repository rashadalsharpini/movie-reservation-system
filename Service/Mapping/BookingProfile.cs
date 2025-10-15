using AutoMapper;
using Domain.Entities;
using Shared.Dtos;

namespace Service.Mapping;

public class BookingProfile:Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, BookingDto>()
            .ForMember(bd => bd.ShowDateTime, opt => opt.MapFrom(b => b.Tickets.First().Schedule.ShowDateTime))
            .ForMember(bd => bd.MovieName, opt => opt.MapFrom(b => b.Tickets.First().Schedule.Movie.Name))
            .ForMember(bd => bd.CinemaName, opt => opt.MapFrom(b => b.Tickets.First().Schedule.Hall.Cinema.Name))
            .ForMember(bd => bd.HallName, opt => opt.MapFrom(b => b.Tickets.First().Schedule.Hall.Name))
            .ForMember(bd => bd.TicketCount, opt => opt.MapFrom(b => b.Tickets.First().Schedule.Tickets.Count))
            .ForMember(bd => bd.UserEmail, opt => opt.Ignore());
        CreateMap<Booking, BookingDetailsDto>()
            .ForMember(bd => bd.ShowDateTime, opt => opt.MapFrom(b => b.Tickets.First().Schedule.ShowDateTime))
            .ForMember(bd => bd.MovieName, opt => opt.MapFrom(b => b.Tickets.First().Schedule.Movie.Name))
            .ForMember(bd => bd.CinemaName, opt => opt.MapFrom(b => b.Tickets.First().Schedule.Hall.Cinema.Name))
            .ForMember(bd => bd.HallName, opt => opt.MapFrom(b => b.Tickets.First().Schedule.Hall.Name))
            .ForMember(bd => bd.DurationMinutes,
                opt => opt.MapFrom(b => b.Tickets.First().Schedule.Movie.DurationMinutes))
            .ForMember(bd => bd.MovieDescription, opt => opt.MapFrom(b => b.Tickets.First().Schedule.Movie.Description))
            .ForMember(bd => bd.CinemaLocation,
                opt => opt.MapFrom(b => b.Tickets.First().Schedule.Hall.Cinema.Location));
        CreateMap<Ticket, TicketDto>()
            .ForMember(td => td.SeatNumber, opt => opt.MapFrom(t => t.Seat.SeatNumber))
            .ForMember(td => td.SeatRow, opt => opt.MapFrom(td => td.Seat.SeatRow));

    }
}