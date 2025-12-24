global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;


global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Project.DAL.Entities;

global using static System.Runtime.InteropServices.JavaScript.JSType;

global using Project.BLL.Contracts;
global using Project.BLL.Abstractions;
global using System.Security.Cryptography;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Logging;
global using Project.BLL.Authantication;
global using Microsoft.AspNetCore.Http;
global using Project.BLL.Errors;
global using Error = Project.BLL.Abstractions.Error;
global using AutoMapper;
global using Microsoft.AspNetCore.WebUtilities;
global using Microsoft.EntityFrameworkCore;
global using Project.BLL.IServices;
global using Project.BLL.DTOS;
global using Project.DAL.IRepository;





