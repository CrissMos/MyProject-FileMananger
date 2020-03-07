import { Component } from '@angular/core';
import { Asset } from '../models/asset.model';
import { AssetCategory } from '../models/assetCategory.enum';
import { AssetService } from '../Services/asset.service';
import { Folder } from '../models/folder.model';
import { FolderService } from '../services/folder.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',

})
export class HomeComponent {
    asset: Asset = {
        id: null,
        name: "",
        category: AssetCategory.Image,
        folderId: null,
        variantToDisplayLink: ""
    }

    folder: Folder = {
        id: null,
        parentId: null,
        name: ""
    }

    assets: Asset[] = [];
    folders: Folder[] = [];

    parentId: string = null;

    constructor(
        private assetService: AssetService,
        private folderService: FolderService
    ) { }

    ngOnInit() {
        this.getFolders();
        this.getAssets();
    }

    public addAsset(name: string): void {
        if (!name) { return; }
        this.asset.name = name;
        this.assetService.addAsset(this.asset)
            .subscribe(data => {
                console.log(data);
                this.getAssets();
            });
    }

    public getAssets() {
        this.assetService.getAssets().subscribe(data => {
            this.assets = data;
            console.log(this.assets);
        });
    }

    public addFolder(name: string): void {
        if (!name) { return; }
        this.folder.name = name;
        this.folderService.addFolder(this.folder)
            .subscribe(data => {
                console.log(data);
                this.getFolders();
            });
    }

    public getFolders() {
        this.folderService.getFolders().subscribe(data => {
            this.folders = data;
            console.log(this.assets);
            this.parentId = this.folders[0].id
        });
    }

    public onFolder(folder: Folder) {

    }
}
