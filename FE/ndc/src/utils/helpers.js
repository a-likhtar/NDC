// Генерация массива годов от 1900 до текущего
const currentYear = new Date().getFullYear()
const years = []

for (let year = currentYear; year >= 1900; year--) {
    years.push({ value: year, label: year.toString() })
}

export function generateRange(from, to) {
    const years = [];

    for (let year = from.getFullYear(); year <= to.getFullYear(); year++) {
        years.push({ value: year, label: year.toString() });
    }

    return years;
}