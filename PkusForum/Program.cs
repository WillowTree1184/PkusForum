using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PkusForum.Services.CookieAuthService;
using PkusForum.Services.DatabaseService;
using PkusForum.Services.DatabaseService.Managers;
using PkusForum.Services.DatabaseService.TableStruct;

string webUrl = "https://192.168.2.6:7782";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthController>();

Database.Initialize();

AccountManager.TryRegist(new Account()
{
    AccountId = "admin",
    PasswordHash = Sha256Calculator.GetSha256Value("pfadmin176"),
    UserName = "PkusForum �ٷ�",
    SelfIntroduction = "������ PkusForum �ٷ��˺ţ���ϵ���ǡ������� Bug �����뵽 QQ Ⱥ 922510834",
    UserPermission = Account.Permission.Admin
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run(webUrl);
