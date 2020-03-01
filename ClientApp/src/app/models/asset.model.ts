import { AssetCategory } from "./assetCategory.enum"

export interface Asset {
    id?: null;
    category: AssetCategory;
    name: string;
    folderId: string;
}
