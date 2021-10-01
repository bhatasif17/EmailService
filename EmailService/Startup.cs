/* ------------------------------------------------------------------
 * The GNU General Public License v3.0
 * Copyright (C) 2007 Free Software Foundation, Inc. <https://fsf.org/>
 * Asif Bhat
   ------------------------------------------------------------------ */
using EmailService.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace EmailService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //---Gmail
            var from = Configuration.GetSection("Email")["From"];

            var gmailSender = Configuration.GetSection("Gmail")["Sender"];
            var gmailPassword = Configuration.GetSection("Gmail")["Password"];
            var gmailPort = Convert.ToInt32(Configuration.GetSection("Gmail")["Port"]);

            //---Sendgrid
            var sendGridSender = Configuration.GetSection("Sendgrid")["Sender"];
            var sendGridKey = Configuration.GetSection("Sendgrid")["SendgridKey"];

            //--Gmail
            //services
            //    .AddFluentEmail(gmailSender, from)
            //    .AddRazorRenderer()
            //    .AddSmtpSender(new SmtpClient("smtp.gmail.com")
            //    {
            //        UseDefaultCredentials = false,
            //        Port = gmailPort,
            //        Credentials = new NetworkCredential(gmailSender, gmailPassword),
            //        EnableSsl = true,
            //    });

            services
                .AddFluentEmail(sendGridSender, from)
                .AddRazorRenderer()
                .AddSendGridSender(sendGridKey);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Email Service", Version = "v1" });
            });

            services.AddScoped<IMailSender, MailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Email Service v1"));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
