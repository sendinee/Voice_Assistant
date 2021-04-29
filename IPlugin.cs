using System.Threading.Tasks;
using System.Collections;

namespace voice_assistant {
    interface IPlugin {
        Task doSomething(ArrayList args);
    }
}