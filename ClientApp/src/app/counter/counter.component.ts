import { Component } from '@angular/core';
import { AssetService } from '../Services/asset.service';
import { Asset } from '../models/asset.model';
import { AssetCategory } from '../models/assetCategory.enum';

@Component({
    selector: 'app-counter-component',
    templateUrl: './counter.component.html'
})
export class CounterComponent {
    asset: Asset = {
        id: null,
        name: "",
        category: AssetCategory.Image,
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
                //this.getAssets();
            });
    }

    public getAssets() {
        this.assetService.getAssets().subscribe(data => {
            this.assets = data;
            console.log(this.assets);
        });
    }
}
