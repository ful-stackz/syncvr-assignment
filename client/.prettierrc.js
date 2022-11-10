/** @type {import('prettier').Config } */
module.exports = {
  endOfLine: 'lf',
  quoteProps: 'consistent',
  semi: false,
  singleQuote: true,
  tabWidth: 2,
  trailingComma: 'es5',
  useTabs: false,
  plugins: [require('prettier-plugin-tailwindcss')],
}
