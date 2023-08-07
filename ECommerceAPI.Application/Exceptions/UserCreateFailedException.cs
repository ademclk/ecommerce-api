using System;
namespace ECommerceAPI.Application.Exceptions;

public class UserCreateFailedException : Exception
{
	public UserCreateFailedException() : base("Unexpected error occured while creating user.")
	{
	}

    public UserCreateFailedException(string? message) : base(message)
    {
    }

    public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

