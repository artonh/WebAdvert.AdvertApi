﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Route("adverts/v1")] //[Route("api/v1/[controller]")]
    public class Advert : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;

        public Advert(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(400)]
        [ProducesResponseType(201, Type = typeof(CreateAdvertResponse))]
        public async Task<IActionResult> CreateAsync(AdvertModel model)
        {
            string recordId;
            try
            {
                recordId = await _advertStorageService.Add(model);
            }
            catch (KeyNotFoundException e)
            {
                return new NotFoundResult();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
                throw;
            }

            return StatusCode(201, new CreateAdvertResponse { Id = recordId });
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {
                await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException e)
            {
                return new NotFoundResult();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

            return new OkResult();
        }

    }
}