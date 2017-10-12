using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace ListaDeCompras.Util
{
    public class LocalStorage
    {
        public async static Task<bool> ArquivoExisteAsync(string nomeDoArquivo, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult pastaExiste = await pasta.CheckExistsAsync(nomeDoArquivo);

            return pastaExiste == ExistenceCheckResult.FileExists;
        }

        public async static Task<bool> PastaExisteAsync(string nomeDaPasta, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult pastaExiste = await pasta.CheckExistsAsync(nomeDaPasta);

            return pastaExiste == ExistenceCheckResult.FolderExists;
        }

        public async static Task<IFolder> CriarPastaAsync(string nomeDaPasta, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            return await pasta.CreateFolderAsync(nomeDaPasta, CreationCollisionOption.ReplaceExisting);
        }

        public async static Task<IFile> CriarArquivoAsync(string nomeDoArquivo, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;
            return await pasta.CreateFileAsync(nomeDoArquivo, CreationCollisionOption.ReplaceExisting);
        }

        public async static Task<bool> EscreverTextoAsync(string nomeDoArquivo, string texto = "", IFolder pastaRaiz = null)
        {
            IFile arquivo = await CriarArquivoAsync(nomeDoArquivo, pastaRaiz);
            await arquivo.WriteAllTextAsync(texto);
            return true;
        }

        public async static Task<string> LerTextoAsync(string nomeDoArquivo, IFolder pastaRaiz = null)
        {
            string texto = "";
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;

            if (await ArquivoExisteAsync(nomeDoArquivo, pasta))
            {
                try
                {
                    IFile arquivo = await pasta.GetFileAsync(nomeDoArquivo);
                    texto = await arquivo.ReadAllTextAsync();
                }
                catch
                {
                    throw new Exception("Erro ao ler arquivo.");
                }
            }

            return texto;
        }

        public async static Task<bool> RemoverArquivoAsync(string nomeDoArquivo, IFolder pastaRaiz = null)
        {
            IFolder pasta = pastaRaiz ?? FileSystem.Current.LocalStorage;

            if (await ArquivoExisteAsync(nomeDoArquivo, pasta))
            {
                IFile arquivo = await pasta.GetFileAsync(nomeDoArquivo);
                await arquivo.DeleteAsync();
                return true;
            }

            return false;
        }
    }
}
