export const ShrinkText = (text?: string) => {
  if (!text) return '';
  return text.length <= 60 ? text : text.substr(0, 60) + '...';
};
