import { VariantType } from "./variantType.enum";

export interface Variant {
	id: number;
	width: number;
	height: number;
	type: VariantType;
	link: string;
	AssetId: null;
}
