﻿using MediatR;

namespace MvDb.Application.Actions.Directors.Commands.Delete;

public record DeleteDirectorCommand(int Id) : IRequest;