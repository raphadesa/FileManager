#pragma checksum "D:\Projets\Blazor\FileManager\FileManager\Pages\FileManagr.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f64fea24d03278960b8eda92e2d21031c5cfc6ce"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace FileManager.FileManager.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Projets\Blazor\FileManager\_Imports.razor"
using System.Net;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using System.IO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using FileManager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using FileManager.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using FileManager.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using FileManager.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using FileManager.Helpers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using FileManager.Helpers.DragAndDrop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using FileManager.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using BlazorContextMenu;

#line default
#line hidden
#nullable disable
#nullable restore
#line 20 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using BlazorStrap;

#line default
#line hidden
#nullable disable
#nullable restore
#line 21 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Blazored.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 22 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Blazored.Toast.Configuration;

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Blazored.Toast.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 24 "D:\Projets\Blazor\FileManager\FileManager\_Imports.razor"
using Blazor.FileReader;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class FileManagr : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 96 "D:\Projets\Blazor\FileManager\FileManager\Pages\FileManagr.razor"
       

    List<DirectoryVM> directories;
    string filePath { get; set; }
    BSModal ConfirmationModal;
    BSModal UploadModal;
    FileVM currentFile { get; set; }
    DirectoryVM currentDir { get; set; }
    bool isFolder { get; set; } = true;
    Upload myUpload;
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            RefreshUI();
        }
    }

    void RefreshUI()
    {
        var vm = fileManagerService.getTreeData();
        directories = vm.tree;
        currentDir = vm.tree[0];
        StateHasChanged();
    }

    /*File management */

    void selectFile(FileVM file)
    {
        isFolder = false;
        currentFile = file;
    }

    void selectDir(DirectoryVM dir)
    {
        isFolder = true;
        currentDir = dir;
    }

    void deleteFile(FileVM file)
    {
        fileManagerService.deleteFile(file.fullName);
        file.showMenu = false;
        file.isDeleted = true;
        StateHasChanged();        
    }

    void prepareRenameFile(FileVM file)
    {
        file.isRename = !file.isRename;
        file.renameFile = file.name;
    }

    void renameFile(FileVM file)
    {
        fileManagerService.renameFile(file);
        file.name = file.renameFile;
        file.isRename = false;
        RefreshUI();
    }

    async Task downloadFile(FileVM file)
    {
        await JsRuntime.InvokeAsync<Task>("filemanager.NavigateTo", $"/FileManager/Download/{file.fullName.EncryptBase64()}");
    }

    void uploadFile(DirectoryVM dir)
    {
        filePath = dir.fullName;
        UploadModal.Show();
    }


    void finishUpload(List<string> files)
    {
        UploadModal.Hide();
        toastService.ShowSuccess("Your files were successfully uploaded!", "Information");
        RefreshUI();
        myUpload = null;
    }

    void dragAndDropFile(FilesVM vm)
    {
        fileManagerService.MoveFile(vm.origFile, vm.destFile);
        RefreshUI();

    }

    /*Folder Management */

    void deleteDir(DirectoryVM dir)
    {
        currentDir = dir;
        dir.showMenu = false;
        ConfirmationModal.Show();
    }

    void DeleteConfirm()
    {
        ConfirmationModal.Hide();
        fileManagerService.deleteDirectory(currentDir.fullName);
        currentDir.isDeleted = true;
        StateHasChanged();        
    }

    void prepareRenameDir(DirectoryVM dir)
    {
        dir.isRename = !dir.isRename;
        dir.renameDir = dir.name;
    }

    void renameDir(DirectoryVM dir)
    {
        fileManagerService.renameDir(dir);
        RefreshUI();
    }

    void prepareCreateDir(DirectoryVM dir)
    {
        dir.isCreate = !dir.isCreate;
    }

    void createDir(DirectoryVM dir)
    {
        var path = Path.Combine(dir.fullName, dir.createDir);
        if (!Directory.Exists(path))
        {
            fileManagerService.createDir(dir);
            RefreshUI();
        }
        else
            toastService.ShowError("Thanks for selecting a valid directory !", "Information");
    }

   void dragAndDropDir(DirectoriesVM vm)
    {
        fileManagerService.dragAndDropDir(vm);
        RefreshUI();
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IToastService toastService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private FileManagerService fileManagerService { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JsRuntime { get; set; }
    }
}
#pragma warning restore 1591
