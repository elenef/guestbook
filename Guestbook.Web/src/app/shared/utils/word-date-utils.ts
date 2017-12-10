import { WordUtils } from '../word-utils'
export class WordDateUtils {
    /**
     * Получить количество дней
     * @param days количество дней
     */
    static getForm(days: number) {
        let wordDay = "";
        if (days == 0) {
            wordDay = "сегодня";
        }
        else if (days == 1) {
            wordDay = "завтра";
        }
        else if (days == 2) {
            wordDay = "послезавтра"
        }
        else if(days < 0){
            wordDay = "просрочка " + WordUtils.GetForm(Math.abs(days), 'д', ['ень', 'ня', 'ней']);
        }
        else {
            wordDay = "через " + WordUtils.GetForm(days, 'д', ['ень', 'ня', 'ней']);
        }
        return wordDay;
    }
}