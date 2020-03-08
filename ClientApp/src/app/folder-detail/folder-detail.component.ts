import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Asset } from '../models/asset.model';
import { AssetCategory } from '../models/assetCategory.enum';
import { AssetService } from '../Services/asset.service';
import { map } from 'rxjs/operators';

@Component({
    selector: 'folder-detail',
    templateUrl: './folder-detail.component.html'
})

export class FolderDetailComponent {
    id: string = null;
    asset: Asset = {
        id: null,
        name: "",
        category: AssetCategory.Image,
        folderId: null,
        variantToDisplayLink: ""
    };

    thumbnailAssets: Asset[] = [];

    selectedFile: File = null;

    constructor(
        private route: ActivatedRoute,
        private assetService: AssetService
    )
    {
        this.route.params.subscribe(params => {
            this.id = params['id']
        })
        console.log(this.id);
        this.getThumbnailAssets(this.id);
    }
    

    ngOnInit() {
       
    }

    public addAsset(name: string): void {
        if (!name) { return; }
        this.asset.name = name;
        this.asset.folderId = this.id;
        this.assetService.addAsset(this.asset)
            .subscribe(data => {
                console.log(data);
                this.getThumbnailAssets(this.id);
            });
    }

    public getThumbnailAssets(folderId: string) {
        this.assetService.getAssetsWithThumbnailView(folderId)
            .subscribe(data => {
                this.thumbnailAssets = data;
                console.log(this.thumbnailAssets);
            })
    }

    public onFileSelected(files) {
        if (files.length === 0) {
            return;
        }

        const formData = new FormData();

        formData.append(files[0].name, files[0]);

        this.assetService.upload(formData, this.id)
            .subscribe(data => {
                console.log(data);
            })
    }
}
