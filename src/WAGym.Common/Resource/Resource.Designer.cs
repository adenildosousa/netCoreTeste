﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WAGym.Common.Resource {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WAGym.Common.Resource.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ocorreu um erro ao salvar. Tente novamente..
        /// </summary>
        public static string ErrorSaving {
            get {
                return ResourceManager.GetString("ErrorSaving", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Senha inválida..
        /// </summary>
        public static string InvalidPassword {
            get {
                return ResourceManager.GetString("InvalidPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Request inválido.
        /// </summary>
        public static string InvalidRequest {
            get {
                return ResourceManager.GetString("InvalidRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A Sessão do usuário expirou. Realize login novamente..
        /// </summary>
        public static string NoActiveSessions {
            get {
                return ResourceManager.GetString("NoActiveSessions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Apenas perfis existentes e ativos são aceitos..
        /// </summary>
        public static string OnlyExistingProfilesAreAllowed {
            get {
                return ResourceManager.GetString("OnlyExistingProfilesAreAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Apenas 1 perfil deve ser selecionado como padrão!.
        /// </summary>
        public static string OnlyOneProfileCanBeDefault {
            get {
                return ResourceManager.GetString("OnlyOneProfileCanBeDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O perfil do usuário não possui acesso a funcionalidade: {0}..
        /// </summary>
        public static string ProfileDoesNotHaveAccessToFunctionality {
            get {
                return ResourceManager.GetString("ProfileDoesNotHaveAccessToFunctionality", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não há perfis informados. Necessário informar ao menos 1 perfil..
        /// </summary>
        public static string ProfilesReportedDoesNotExists {
            get {
                return ResourceManager.GetString("ProfilesReportedDoesNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O Perfil do usuário não possui acesso a nenhuma funcionalidade..
        /// </summary>
        public static string ProfileWithoutFunctionalities {
            get {
                return ResourceManager.GetString("ProfileWithoutFunctionalities", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A Sessão do usuário expirou. Realize login novamente..
        /// </summary>
        public static string SessionExpired {
            get {
                return ResourceManager.GetString("SessionExpired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O Status fornecido não existe..
        /// </summary>
        public static string StatusNotFound {
            get {
                return ResourceManager.GetString("StatusNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuário já existe no banco de dados..
        /// </summary>
        public static string UserAlreadyExists {
            get {
                return ResourceManager.GetString("UserAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuário não encontrado..
        /// </summary>
        public static string UserNotFound {
            get {
                return ResourceManager.GetString("UserNotFound", resourceCulture);
            }
        }
    }
}