﻿using System;
using MediatR;

namespace GoodNews.Infrastructure.Commands.Models.News
{
    public class DeleteNewsCommandModel : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteNewsCommandModel(Guid id)
        {
            Id = id;
        }
    }
}