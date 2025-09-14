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
    const string token = "";

    private static DiscordSocketClient _client;

    public static async Task Main(){
        _client = new DiscordSocketClient();
        _client.Log += Log;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        _client.Ready += Client_ReadyAsync;
        await Task.Delay(-1);
    }

    private static Task Log(LogMessage msg){
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private static async Task Client_ReadyAsync() {
        // guild(サーバー)用のコマンド
        var guild = _client.GetGuild(1376102932860244029);
        _client.SlashCommandExecuted += SlashCommandHandler;

        var commands = await guild.GetApplicationCommandsAsync();

        // globalコマンド
        var globalCommand = new SlashCommandBuilder();
        globalCommand.WithName("snowsan_cmd");
        globalCommand.WithDescription("スノーさんのコマンド");

        // ビルダーからコマンドを構成する
        try {
            // guildが不要なのでSocketClientからコールする
            await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
        }
        catch (HttpException exception){
            // エラー
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

            Console.WriteLine(json);
        }
    }

    private static async Task SlashCommandHandler(SocketSlashCommand command) {
        if (command.CommandName.Equals("snowsan_cmd")) {
            Console.WriteLine(command.CommandName);
            await command.RespondAsync($"実行できました {command.Data.Name}");
        }
        
    }
}