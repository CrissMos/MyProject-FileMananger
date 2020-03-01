import { Component } from '@angular/core';
import { Asset } from '../models/asset.model';
import { AssetCategory } from '../models/assetCategory.enum';
import { AssetService } from '../Services/asset.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',

})
export class HomeComponent {
    asset: Asset = {
        id: null,
        name: "",
        category: AssetCategory.Image,
        folderId: null
    }

    public currentCount = 0;
    assets: Asset[] = [];

    constructor(
        private assetService: AssetService) { }

    ngOnInit() {
        this.getAssets();
    }

    public incrementCounter() {
        this.currentCount++;
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
}
