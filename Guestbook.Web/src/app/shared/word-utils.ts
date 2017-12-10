export class WordUtils {
    /**
     * Получить множественное число существительного
     * @param count Число объектов
     * @param base Основа слова
     * @param endings Список окончаний для разных форм
     * На примере слова Пользователь:
     * base = пользовател; 
     * endings = [ь, я, ей]; 
     * 0 - 1 пользователЬ, 21 пользователЬ; 
     * 1 - 2 пользователЯ, 33 пользователЯ; 
     * 2 - 5 пользователЕЙ, 10 пользователЕЙ
     */
    static GetForm(count: number, base: string, endings: string[]) {
        let lastDigit = count % 100;
        let ending = '';
        if (count === 0) {
            return 'Нет ' + base + endings[2];
        } else {
            if (lastDigit >= 11 && lastDigit <= 19) {
                ending = endings[2];
            } else {
                lastDigit = count % 10;
                if (lastDigit === 1) {
                    ending = endings[0];
                } else if (lastDigit === 2 || lastDigit === 3 || lastDigit === 4) {
                    ending = endings[1];
                } else {
                    ending = endings[2];
                }
            }
            return count + ' ' + base + ending;
        }
    }
}
