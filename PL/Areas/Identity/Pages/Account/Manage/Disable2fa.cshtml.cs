﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace PL.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<Disable2faModel> _logger;

        public Disable2faModel(
            UserManager<IdentityUser> userManager,
            ILogger<Disable2faModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se puede cargar el usuario con Identificación '{_userManager.GetUserId(User)}'."); /*Unable to load user with ID*/
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException($"No se puede desactivar 2FA para el usuario porque actualmente no está habilitado."); /*Cannot disable 2FA for user as it's not currently enabled*/
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se puede cargar el usuario con Identificación '{_userManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Se produjo un error inesperado al desactivar 2FA.");/*"Unexpected error occurred disabling 2FA.*/
            }

            _logger.LogInformation("El usuario con identificación '{UserId}' ha deshabilitado 2fa.", _userManager.GetUserId(User));//User with ID '{UserId}' has disabled 2fa.
            StatusMessage = "2fa ha sido deshabilitado. Puede volver a habilitar 2fa cuando configura una aplicación de autenticación"; //2fa has been disabled. You can reenable 2fa when you setup an authenticator app
            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}
