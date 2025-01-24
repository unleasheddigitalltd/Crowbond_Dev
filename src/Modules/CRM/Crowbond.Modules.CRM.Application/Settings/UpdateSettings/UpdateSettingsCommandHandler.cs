using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Abstractions.Data;
using Crowbond.Modules.CRM.Domain.Settings;

namespace Crowbond.Modules.CRM.Application.Settings.UpdateSettings;

internal sealed class UpdateSettingsCommandHandler(
    ISettingRepository settingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSettingsCommand>
{
    public async Task<Result> Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the existing setting
        Setting setting = await settingRepository.GetAsync(cancellationToken);

        // Check if the setting exists
        if (setting == null)
        {
            return Result.Failure(SettingErrors.NotFound);
        }

        // Check if there is a change in PaymentTerms
        if (request.PaymentTerms != null && request.PaymentTerms != setting.PaymentTerms)
        {
            // Create a new setting and update the repository
            var newSetting = Setting.Create(request.PaymentTerms.Value);

            settingRepository.Remove(setting);
            settingRepository.Insert(newSetting);

            // Persist the changes
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();

    }
}
