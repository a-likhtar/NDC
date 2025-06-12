export function round(value, decimals = 0) {
    if (typeof value !== 'number' || isNaN(value)) return '';
    const factor = Math.pow(10, decimals);
    return Math.round(value * factor) / factor;
}