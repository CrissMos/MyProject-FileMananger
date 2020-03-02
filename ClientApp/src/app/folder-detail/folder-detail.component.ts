import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'folder-detail',
    templateUrl: './folder-detail.component.html'
})

export class FolderDetailComponent {
    id: string = null;

    constructor(
        private route: ActivatedRoute
    )
    {
        this.route.params.subscribe(params => {
            this.id = params['id']
        })
        console.log(this.id);
    }
    

    ngOnInit() {
        
    }
}
