export * from './effects/meals.effects';
import * as fromMeals from './reducers/meals.reducer';
import * as MealsActions from './actions/meals.action';
import * as fromMealsSelectors from './selectors/meals.selector';

export { fromMeals, MealsActions, fromMealsSelectors };
