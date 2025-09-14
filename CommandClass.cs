using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondBot {
    internal class CommandClass : ModuleBase{
        [Command("hi")]
        public async Task Reply() {
            await ReplyAsync("hello");
        }
    }
}