import {Observable} from 'rxjs/Rx';

export interface IModelService<TModel> {
    model: TModel;
    newItem: boolean;

    init(modelId: string): Observable<TModel>;
    saveChanges(isNewItem?: boolean): Observable<TModel>;
    delete(): Observable<void>;
}