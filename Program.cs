using ConsoleApp2;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.Threading.Tasks;

class Program {
    static string bot_token;

    private static DiscordSocketClient client;

    public static async Task Main(){
        bot_token = ConfigManager.GetConfig().DiscordToken;

        client = new DiscordSocketClient();
        client.Log += Log;
        ConfigManager.CreateConfig();

        await client.LoginAsync(TokenType.Bot, bot_token);
        await client.StartAsync();

        var globalCommand = new SlashCommandBuilder();
        globalCommand.WithName("guild_info");
        globalCommand.WithDescription("サーバーの情報取得するコマンド");

        client.Ready += async () =>
        {
            try
            {
                client.SlashCommandExecuted += SlashCommandHandler;
                await client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラーが発生しました:" + ex.Message);
                Environment.Exit(-1);
            }
        };

        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg){
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private static async Task SlashCommandHandler(SocketSlashCommand command) {
        if (command.CommandName.Equals("guild_info")) {
            ulong? guildid = command.GuildId;
            var guild = client.GetGuild((ulong) guildid);
            var embed = new EmbedBuilder();
            embed.Title = "サーバーの情報";
            embed.AddField("サーバーの名前", guild.Name);
            var channel = command.Channel;
            await channel.SendMessageAsync(embed: embed.Build());
        }
        
    }
}